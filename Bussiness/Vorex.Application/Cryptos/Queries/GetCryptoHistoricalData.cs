using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Domain.Cryptos;
using Vorex.Domain.Cryptos.Specifications;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Cryptos.Queries;

public class GetCryptoHistoricalData
{
    public sealed class Query : IRequest<Result<List<Data>, Error>>
    {
        public Guid CryptoId { get; private set; }

        public DateOnly? StartDate { get; private set; }

        public DateOnly? EndDate { get; private set; }

        private Query(Guid cryptoId, DateOnly? startDate, DateOnly? endDate)
        {
            CryptoId = cryptoId;
            StartDate = startDate;
            EndDate = endDate;
        }

        public static Query Create(Guid cryptoId, DateOnly? startDate, DateOnly? endDate) => new(cryptoId,startDate,endDate);
    }

    public sealed class Data
    {
        public Guid Id { get; set; }
        public DateOnly Date { get; set; }
        public decimal ClosingPrice { get; set; }
        public CryptoHistoricalPriceDto ToCryptoHistoricalPriceDto()
        {
            return new CryptoHistoricalPriceDto
            {
                Id = Id,
                Date = Date,
                ClosingPrice = ClosingPrice
            }; 
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo) : IRequestHandler<Query, Result<List<Data>, Error>>
    {
        public Task<Result<List<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {

            var crypto = _cryptoRepo.Find(new CryptoWithHistoricalDataSpecification(request.CryptoId)).FirstOrDefault();

            if (crypto == null)
                return Task.FromResult<Result<List<Data>, Error>>
                    (Error.NotFound(nameof(GetCryptoHistoricalData), nameof(Crypto.Id), request.CryptoId.ToString()));

            var data = crypto.HistoricalPrices
            .Where(chp =>
                (!request.StartDate.HasValue || chp.Date >= request.StartDate.Value) &&
                (!request.EndDate.HasValue || chp.Date <= request.EndDate.Value)
            )
            .Select(chp => new Data
            {
                Id = chp.Id,
                ClosingPrice = chp.ClosingPrice,
                Date = chp.Date
            })
            .ToList();


            return Task.FromResult<Result<List<Data>, Error>>(data);
        }
    }
}
