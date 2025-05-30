using MediatR;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;
using Vorex.Domain.User;
using System.Linq;

namespace Vorex.Application.Users.Commands;

public class RemoveCryptoFromFavourite
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public Guid CryptoId { get; set; }

        public Guid UserId { get; set; }

        private Command(Guid cryptoId, Guid userId)
        {
            CryptoId = cryptoId;
            UserId = userId;
        }

        public static Command Create(Guid cryptoId, Guid userId)
        {
            return new Command(cryptoId, userId);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IReadOnlyRepository<Domain.User.User> userReadOnlyRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandel = CanHandle(request);
            if (canHandel.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandel);

            var user = _userRepo.Find(new UserWithCryptoFavourite(request.UserId)).FirstOrDefault();

            var removeCryptoFromFavouriteResult = user!.RemoveFromFavorites(request.CryptoId);

            return Task.FromResult<Result<bool, Error>>(removeCryptoFromFavouriteResult);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var user = userReadOnlyRepo.Find(new UserWithCryptoFavourite(command.UserId)).FirstOrDefault();

            if (!user!.Favorites.Any(f=>f.CryptoId == f.CryptoId))
                return Error.NotFound(nameof(RemoveCryptoFromFavourite), nameof(Domain.User.CryptoFavorite.CryptoId), command.CryptoId.ToString());

            return true;
        }
    }
}