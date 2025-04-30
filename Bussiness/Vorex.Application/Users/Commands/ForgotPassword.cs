using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Commands;

public class ForgotPassword
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

    public sealed class Handler(IReadOnlyRepository<Domain.User.User> _userRepo, IJwtService _jwtService, IEmailService _emailService) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = _userRepo.GetAll().FirstOrDefault(u=>u.Email==request.Email && u.IsEmailConfirmed);

            if (user is null)
                return Task.FromResult<Result<bool, Error>>(Error.NotFound(nameof(ForgotPassword), nameof(Domain.User.User.Email), request.Email));

            var resetToken = _jwtService.GenerateEmailVerificationToken(user.Id, user.Email);
            _emailService.SendResetPasswordEmaill(user.Email, $"{user.FirstName} {user.LastName}", $"reset-password?token={resetToken}");

            return Task.FromResult<Result<bool, Error>>(true);
        }

    }
}
