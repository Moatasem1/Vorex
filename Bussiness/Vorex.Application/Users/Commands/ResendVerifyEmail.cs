using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User;

namespace Vorex.Application.Users.Commands;

public class ResendVerifyEmail
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public string Email { get; private set; }

        private Command(string email)
        {
            Email = email;
        }

        public static Command Create(string email)
        {
            return new Command(email);
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.User.User> _userRepo, IEmailService _emailService, IJwtService _jwtService) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = _userRepo.GetAll().FirstOrDefault(u => u.Email == request.Email);

            if (user is null)
                return Task.FromResult<Result<bool, Error>>(Error.NotFound(nameof(ResendVerifyEmail),nameof(User.Email),request.Email));

            if (user.IsEmailConfirmed)
                return Task.FromResult<Result<bool, Error>>(Error.EmailVerified(nameof(ResendVerifyEmail), nameof(User.Email),user.Email));

            var verficationToken = _jwtService.GenerateEmailVerificationToken(user.Id, user.Email);
            _emailService.SendVerificationEmail(user.Email, $"{user.FirstName} {user.LastName}", $"verify-email?token={verficationToken}");
            return Task.FromResult<Result<bool, Error>>(true);
        } 
    }
}
