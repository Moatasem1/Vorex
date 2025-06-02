using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Domain.Cryptos;
using Vorex.Domain.Cryptos.Specifications;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Cryptos.Queries;

public class GetCryptoHistoricalData
{
    public sealed class Query : IRequest<Result<Data, Error>>
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
        public DateOnly MinDate { get; set; }
        public DateOnly MaxDate { get; set; }
        public DateOnly StartDate { get; set; }
        public DateOnly EndDate { get; set; }
        public required List<CryptoHistoricalPriceItemDto> Info { get; set; }

        public CryptoHistoricalPriceDto ToCryptoHistoricalPriceDto()
        {
            return new CryptoHistoricalPriceDto
            {
               MinDate = MinDate,
               MaxDate = MaxDate,
               StartDate = StartDate,
               EndDate = EndDate,
               Data = Info
            }; 
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo) : IRequestHandler<Query, Result<Data, Error>>
    {
        public Task<Result<Data, Error>> Handle(Query request, CancellationToken cancellationToken)
        {

            var crypto = _cryptoRepo.Find(new CryptoWithHistoricalDataSpecification(request.CryptoId)).FirstOrDefault();

            if (crypto == null)
                return Task.FromResult<Result<Data, Error>>
                    (Error.NotFound(nameof(GetCryptoHistoricalData), nameof(Crypto.Id), request.CryptoId.ToString()));

            var dataQuery = crypto.HistoricalPrices;

            Func<HistoricalPrice, bool> condition;
            if (request.StartDate is null && request.EndDate is null)
            {
                var endDate = dataQuery.Max(c => c.Date);
                var startDate = endDate.AddDays(-30);

                condition = chp =>
                    ( chp.Date >= startDate) &&
                    (chp.Date <= endDate);
            }
            else
            {
                condition = chp =>
                    (!request.StartDate.HasValue || chp.Date >= request.StartDate.Value) &&
                    (!request.EndDate.HasValue || chp.Date <= request.EndDate.Value);
            }

                var data = dataQuery.Where(condition)
                .Select(chp => new CryptoHistoricalPriceItemDto
                {
                    Id = chp.Id,
                    ClosingPrice = chp.ClosingPrice,
                    Date = chp.Date
                }).OrderBy(chp => chp.Date)
                .ToList();

            var finalResult = new Data {
                MinDate = dataQuery.Min(d=>d.Date),
                MaxDate = dataQuery.Max(d=>d.Date),
                StartDate = data.Min(d=>d.Date),
                EndDate = data.Max(d=>d.Date),
                Info = data
            };

            return Task.FromResult<Result<Data, Error>>(finalResult);
        }
    }
}
