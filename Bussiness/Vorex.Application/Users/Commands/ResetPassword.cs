using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.lib;
using System.Security.Claims;

namespace Vorex.Application.Users.Commands;

public class ResetPassword
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public string Token { get; private set; }
        public string NewPassword { get; private set; }

        private Command(string token, string newPassword)
        {
            Token = token;
            NewPassword = newPassword;
        }

        public static Command Create(string token,string newPassword)
        {
            return new Command(token,newPassword);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IJwtService _jwtService) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {

            var userClaims = _jwtService.ValidateToken(request.Token);

            if (userClaims == null)
                return Task.FromResult<Result<bool, Error>>(Error.ValueInvalid(nameof(ResetPassword), nameof(request.Token)));

            var email = userClaims.FindFirst(ClaimTypes.Email)?.Value;

            if (string.IsNullOrWhiteSpace(email))
                return Task.FromResult<Result<bool, Error>>(Error.ValueInvalid(nameof(ResetPassword), nameof(email)));

            var user = _userRepo.GetAll().FirstOrDefault(u => u.Email == email && u.IsEmailConfirmed);

            if (user is null)
                return Task.FromResult<Result<bool, Error>>(Error.NotFound(nameof(ResetPassword), nameof(Domain.User.User.Email), email));

            var changePasswordResult = user.ChangePassword(request.NewPassword);

            return Task.FromResult<Result<bool, Error>>(changePasswordResult);
        }

    }
}
