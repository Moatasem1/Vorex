using MediatR;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Queries;

public class GetAllFavouriteCrypto
{
    public sealed class Query : IRequest<Result<List<Data>, Error>>
    {
        public Guid UserId { get; set; }

        private Query(Guid userId)
        {
            UserId = userId;
        }
        public static Query Create(Guid userId) => new(userId);
    }

    public sealed class Data
    {
        public Guid CryptoId { get; set; }
        public required string CryptoName { get; set; }
        public required string CryptoSymbol { get; set; }

        public FavouriteCryptoDto ToFavouriteCryptoDto()
        {
            return new FavouriteCryptoDto
            {
              Id = CryptoId,
              Symbol = CryptoSymbol,
              Name = CryptoName,
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo, IReadOnlyRepository<Domain.User.User> _userRepo) : IRequestHandler<Query, Result<List<Data>, Error>>
    {
        public Task<Result<List<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = _userRepo.Find(new UserWithCryptoFavourite(request.UserId)).FirstOrDefault();

            var query = from crypto in _cryptoRepo.GetAll().ToList()
                        join favourite in user!.Favorites on crypto.Id equals favourite.CryptoId
                        select new Data
                        {
                            CryptoSymbol= crypto.Symbol,
                            CryptoName = crypto.Name,
                            CryptoId= crypto.Id,
                        };

            return Task.FromResult<Result<List<Data>, Error>>(query.ToList());
        }
    }
}
