using MediatR;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Commands;

public class Logout
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public string RefreshToken { get; private set; }

        private Command(string refreshToken)
        {
            RefreshToken = refreshToken;
        }

        public static Command Create(string refreskToken)
        {
            return new Command(refreskToken);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var user = _userRepo.Find(new VerifiedUserWithRefreshTokens(request.RefreshToken)).FirstOrDefault();

            if (user is null)
                return Task.FromResult<Result<bool, Error>>(Error.NotFound(nameof(Logout), nameof(Domain.User.RefreshToken.Token), request.RefreshToken));

           var removeTokenResult = user.RemoveRefreshToken(request.RefreshToken);

            return Task.FromResult<Result<bool, Error>>(removeTokenResult);
        }
    }

}
