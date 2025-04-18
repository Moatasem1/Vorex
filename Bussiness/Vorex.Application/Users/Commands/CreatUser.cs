using MediatR;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
namespace Vorex.Application.Users.Commands;

public class CreatUser
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

    public sealed class Handler(IRepository<Domain.User.User>_userRepo) : IRequestHandler<Command, Result<Guid, Error>>
    {
        public Task<Result<Guid, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
           var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<Guid, Error>>(canHandle.Error);

            var user = Domain.User.User.Factory.Create(request.FirstName, request.LastName, request.Email, request.Password, request.ProfileImage);

            if (user.IsFailure)
                return Task.FromResult<Result<Guid, Error>>(user.Error);

            _userRepo.Add(user.Value);

            return Task.FromResult<Result<Guid, Error>>(user.Value.Id);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
           var isUserNameExists = _userRepo.GetAll().Any(x => x.FirstName == command.FirstName && x.LastName == command.LastName);

            if (isUserNameExists)
              return Error.ValueAlreadyExists(nameof(CreatUser),$"{nameof(Domain.User.User.FirstName)} and {nameof(Domain.User.User.LastName)}",command.FirstName+" "+command.LastName);

            return true;
        }
    }
}