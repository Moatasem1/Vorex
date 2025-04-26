using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.lib;
using System.Security.Claims;

namespace Vorex.Application.Users.Commands;

public class VerfiyUserEmail
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public string Token { get; private set; }

        private Command(string token)
        {
            Token = token;
        }

        public static Command Create(string token)
        {
            return new Command(token);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IJwtService _jwtService) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var userClaims = _jwtService.ValidateToken(request.Token);

            if (userClaims == null)
                return Task.FromResult<Result<bool, Error>>(Error.ValueInvalid(nameof(VerfiyUserEmail), nameof(request.Token)));

            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                return Task.FromResult<Result<bool, Error>>(Error.ValueInvalid(nameof(VerfiyUserEmail), nameof(request.Token)));

            var user = _userRepo.GetAll().FirstOrDefault(u=>u.Email == email);

            user!.ConfirmEmail();

            _userRepo.Update(user);

            return Task.FromResult<Result<bool, Error>>(true);
        }
    }
}
