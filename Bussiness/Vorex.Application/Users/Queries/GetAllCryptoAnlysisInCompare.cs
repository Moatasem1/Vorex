using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Application.Users.Contracts;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Queries;

public class GetAllCryptoAnlysisInCompare
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
        public Guid CryptoAnlysisHistoryId { get; set; }
        public required string CryptoName { get; set; }
        public required decimal InvestAmount { get; set; }
        public required int HoldingDays { get; set; }
        public decimal Risk { get; set; }
        public required decimal ReturnOfInvestment { get; init; }


        public CryptoAnlysisInComareListDto ToCryptoAnlysisHistoryListDto()
        {
            return new CryptoAnlysisInComareListDto
            {
               CryptoAnlysisHistoryId = CryptoAnlysisHistoryId,
               CryptoName = CryptoName,
               InvestAmount = InvestAmount,
               HoldingDays = HoldingDays,
               Risk = Risk,
               ReturnOfInvestment = ReturnOfInvestment
            };
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepo, IReadOnlyRepository<Domain.User.User> _userRepo, IReadOnlyRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory>cryptoAnlysisHistoryRepo) : IRequestHandler<Query, Result<List<Data>, Error>>
    {
        public Task<Result<List<Data>, Error>> Handle(Query request, CancellationToken cancellationToken)
        {
            var user = _userRepo.Find(new UserWithCryptoCompare(request.UserId)).FirstOrDefault();

            var query = from history in cryptoAnlysisHistoryRepo.GetAll().Where(h => h.UserId == request.UserId).ToList()
                        join compare in user!.Comparisons on history.Id equals compare.CryptoAnalysisHistoryId
                        join crypto in _cryptoRepo.GetAll().ToList() on history.CryptoId equals crypto.Id
                        select new Data
                        {
                            HoldingDays = history.HoldingDays,
                            CryptoAnlysisHistoryId = compare.CryptoAnalysisHistoryId,
                            CryptoName = crypto.Name,
                            InvestAmount = history.Amount,
                            Risk = history.Risk,
                            ReturnOfInvestment = history.Amount * history.Risk + history.Amount 
                        };

            return Task.FromResult<Result<List<Data>, Error>>(query.ToList());
        }
    }
}
