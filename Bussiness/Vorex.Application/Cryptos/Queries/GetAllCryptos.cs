using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Application.others;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Cryptos.Queries;

public class GetAllCryptos
{
    public sealed class Query : IRequest<Result<PaginatedResponse<Data>, Error>>
    {
        public Guid? UserId { get; set; }
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string? SearchQuery { get; private set; }
        private Query(int pageNumber, int pageSize, string? searchQuery, Guid? userId)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchQuery = searchQuery;
            UserId = userId;
        }

        public static Query Create(int pageNumber, int pageSize,string? searchQuery=null,Guid? userId=null) => new(pageNumber, pageSize,searchQuery,userId);
    }

    public sealed class Data
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public required bool IsFavourite { get; set; }
        public CryptoBasicDetailsDto ToCryptoBasicDetailsDto()
        {
            return new CryptoBasicDetailsDto
            {
                Id = Id,
                Name = Name,
                Symbol = Symbol,
                IsFavourite = IsFavourite,
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo, IReadOnlyRepository<Domain.User.User> userRepo) : IRequestHandler<Query, Result<PaginatedResponse<Data>, Error>>
    {
        public Task<Result<PaginatedResponse<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {
            var userFavoriteCryptoIds = new List<Guid>();
            if (request.UserId.HasValue)
            {
                var user = userRepo.Find(new UserWithCryptoFavourite(request.UserId.Value)).FirstOrDefault();
                userFavoriteCryptoIds = user!.Favorites.Select(x=>x.CryptoId).ToList();
            }

            var cryptosQuery = _cryptoRepo.GetAll()
                .Where(x => string.IsNullOrEmpty(request.SearchQuery) || x.Name.Contains(request.SearchQuery) || x.Symbol.Contains(request.SearchQuery));
                
            var totalCount = cryptosQuery.Count();

            var cryptos = cryptosQuery.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data
                {
                    Id = x.Id,
                    Name = x.Name,
                    Symbol = x.Symbol,
                    IsFavourite = userFavoriteCryptoIds.Any(id=>id==x.Id)
                })
                .ToList();


            var result = new PaginatedResponse<Data>(cryptos,totalCount,request.PageNumber,request.PageSize);

            return Task.FromResult<Result<PaginatedResponse<Data>, Error>>(result);
        }
    }
}
