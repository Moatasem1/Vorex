using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Commands;

public class RefreshToken
{
    public sealed class Command : IRequest<Result<RefreshTokenResponse, Error>>
    {
        public string RefreshToken { get; private set; }

        private Command(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public static Command Create(string refreshToken)
        {
            return new Command(refreshToken);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IJwtService _jwtService) : IRequestHandler<Command, Result<RefreshTokenResponse, Error>>
    {
        public Task<Result<RefreshTokenResponse, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
           
            var user = _userRepo.Find(new VerifiedUserWithRefreshTokens(request.RefreshToken)).FirstOrDefault();

            if(user is null)
                return Task.FromResult<Result<RefreshTokenResponse, Error>>(Error.NotFound(nameof(RefreshToken),nameof(Domain.User.RefreshToken.Token),request.RefreshToken));

            var accessToken = _jwtService.GenerateAccessToken(user!);
            var refreshToken = _jwtService.GenerateRefreshToken();
            var refreshTokenExpired = DateTime.UtcNow.AddDays(7);

            var refreshTokenEntity = Domain.User.RefreshToken.Factory.Create(user!.Id, refreshToken, refreshTokenExpired);

            if (refreshTokenEntity.IsFailure)
                return Task.FromResult<Result<RefreshTokenResponse, Error>>(refreshTokenEntity.Error);

            user.RemoveRefreshToken(request.RefreshToken);
            user.AddRefreshToken(refreshTokenEntity.Value);

            var loginResponse = new RefreshTokenResponse
            {
                Expiration = refreshTokenExpired,
                RefreshToken = refreshToken,
                Token = accessToken,
            };

            return Task.FromResult<Result<RefreshTokenResponse, Error>>(loginResponse);
        }

    }
}
