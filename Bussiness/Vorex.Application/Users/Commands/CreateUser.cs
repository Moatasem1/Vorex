using MediatR;
using Microsoft.Extensions.Options;
using Vorex.Application.options;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.lib.Interfaces;
namespace Vorex.Application.Users.Commands;

public class CreateUser
{
    public sealed class Command : IRequest<Result<Guid, Error>>
    {
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public string Email { get; private set; }
        public string Password { get; private set; }
        public string? ProfileImage { get; private set; }

        private Command(string firstName, string lastName, string email, string password, string? profileImage)
        {
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            ProfileImage = profileImage;
        }

        public static Command Create(string firstName, string lastName, string email, string password, string? profileImage)
        {
            return new Command(firstName, lastName, email, password, profileImage);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User>_userRepo,IEmailService _emailService,IJwtService _jwtService,IOptions<JwtOptions>jwtOptions,IPasswordHashService passwordHashService) : IRequestHandler<Command, Result<Guid, Error>>
    {
        public Task<Result<Guid, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
           var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<Guid, Error>>(canHandle.Error);

            var user = Domain.User.User.Factory.Create(request.FirstName, request.LastName, request.Email, request.Password, request.ProfileImage);

            if (user.IsFailure)
                return Task.FromResult<Result<Guid, Error>>(user.Error);

            user.Value.ChangePassword(passwordHashService.HashPassword(user.Value, user.Value.Password));

            _userRepo.Add(user.Value);

           var verficationToken= _jwtService.GenerateEmailVerificationToken(user.Value.Id, user.Value.Email);
            _emailService.SendVerificationEmail(user.Value.Email,$"{user.Value.FirstName} {user.Value.LastName}",$"{jwtOptions.Value.Audience}/auth/email-confirmation?token={verficationToken}");
            return Task.FromResult<Result<Guid, Error>>(user.Value.Id);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var isUserEmailExists = _userRepo.GetAll().Any(x => x.Email == command.Email);

            if(isUserEmailExists)
                return Error.ValueAlreadyExists(nameof(CreateUser), nameof(Domain.User.User.Email), command.Email);

            return true;
        }
    }
}