using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Application.others;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Cryptos.Queries;

public class GetAllCryptos
{
    public sealed class Query : IRequest<Result<PaginatedResponse<Data>, Error>>
    {
        public int PageNumber { get; private set; }
        public int PageSize { get; private set; }
        public string? SearchQuery { get; private set; }
        private Query(int pageNumber, int pageSize, string? searchQuery)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            SearchQuery = searchQuery;
        }

        public static Query Create(int pageNumber, int pageSize,string? searchQuery=null) => new(pageNumber, pageSize,searchQuery);
    }

    public sealed class Data
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public required string Symbol { get; set; }
        public CryptoBasicDetailsDto ToCryptoBasicDetailsDto()
        {
            return new CryptoBasicDetailsDto
            {
                Id = Id,
                Name = Name,
                Symbol = Symbol
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo) : IRequestHandler<Query, Result<PaginatedResponse<Data>, Error>>
    {
        public Task<Result<PaginatedResponse<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {
            var cryptosQuery = _cryptoRepo.GetAll()
                .Where(x => string.IsNullOrEmpty(request.SearchQuery) || x.Name.Contains(request.SearchQuery) || x.Symbol.Contains(request.SearchQuery));
                
            var totalCount = cryptosQuery.Count();

            var cryptos = cryptosQuery.Skip((request.PageNumber - 1) * request.PageSize)
                .Take(request.PageSize)
                .Select(x => new Data
                {
                    Id = x.Id,
                    Name = x.Name,
                    Symbol = x.Symbol
                })
                .ToList();

            var result = new PaginatedResponse<Data>(cryptos,totalCount,request.PageNumber,request.PageSize);

            return Task.FromResult<Result<PaginatedResponse<Data>, Error>>(result);
        }
    }
}
