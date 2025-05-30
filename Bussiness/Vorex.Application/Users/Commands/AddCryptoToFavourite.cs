using MediatR;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;
using Vorex.Domain.User;

namespace Vorex.Application.Users.Commands;

public class AddCryptoToFavourite
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

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IReadOnlyRepository<Domain.Cryptos.Crypto> cryptoReadOnlyRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandel = CanHandle(request);
            if (canHandel.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandel);
            
            var user = _userRepo.Find(new UserWithCryptoFavourite(request.UserId)).FirstOrDefault();

            var isCryptoIdExistInFavourite = user!.Favorites.Any(f=>f.CryptoId==request.CryptoId);

            if (isCryptoIdExistInFavourite)
                return Task.FromResult<Result<bool, Error>>(Error.ValueAlreadyExists(nameof(AddCryptoToFavourite), nameof(CryptoFavorite.CryptoId), request.CryptoId.ToString()));

            var cryptoFavoriteCreationResult = CryptoFavorite.Factory.Create(request.UserId, request.CryptoId);
            if (cryptoFavoriteCreationResult.IsFailure)
                return Task.FromResult<Result<bool, Error>>(cryptoFavoriteCreationResult.Error);
            user.AddToFavorites(cryptoFavoriteCreationResult.Value);

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var crypto = cryptoReadOnlyRepo.GetAll().FirstOrDefault(c=>c.Id==command.CryptoId);

            if (crypto is null)
                return Error.NotFound(nameof(AddCryptoToFavourite), nameof(Domain.Cryptos.Crypto.Id), command.CryptoId.ToString());

            return true;
        }
    }
}
