using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Commands;

public class Login
{
    public sealed class Command : IRequest<Result<LoginResponse, Error>>
    {
        public string Email { get; private set; }
        public string Password { get; private set; }

        private Command(string email, string password)
        {
            Email = email;
            Password = password;
        }

        public static Command Create(string email, string password)
        {
            return new Command(email, password);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IJwtService _jwtService,IPasswordHashService passwordHashService) : IRequestHandler<Command, Result<LoginResponse, Error>>
    {
        public Task<Result<LoginResponse, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<LoginResponse, Error>>(canHandle.Error);

            var user = _userRepo.Find(new VerifiedUserByEmailWithRefreshTokens(request.Email)).FirstOrDefault();

            var accessToken = _jwtService.GenerateAccessToken(user!);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenExpired = DateTime.UtcNow.AddDays(7);

            var refreshTokenEntity = Domain.User.RefreshToken.Factory.Create(user!.Id, refreshToken, refreshTokenExpired);

            if(refreshTokenEntity.IsFailure)
                return Task.FromResult<Result<LoginResponse, Error>>(refreshTokenEntity.Error);

            user.AddRefreshToken(refreshTokenEntity.Value);

            var loginResponse = new LoginResponse
            {
                Expiration = refreshTokenExpired,
                FirstName = user.FirstName,
                LastName = user.LastName,
                RefreshToken = refreshToken,
                Token = accessToken,
            };

            return Task.FromResult<Result<LoginResponse, Error>>(loginResponse);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var user = _userRepo.Find(new VerifiedUserByEmailWithRefreshTokens(command.Email)).FirstOrDefault();

            if (user ==null || !passwordHashService.VerifyPassword(user,command.Password,user.Password))
                return Error.NotFound(nameof(Login), $"{nameof(Domain.User.User.Email)} or {nameof(Domain.User.User.Password)}", command.Email + " or " + command.Password);

            return true;
        }
    }
}
