using MediatR;
using Vorex.Application.services.interfaces;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;
using System.Linq;
using Vorex.Domain.User;

namespace Vorex.Application.Users.Commands;

public class AddCryptoAnylsisToCompare
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public List<Guid> CryptoAnlysisHistoryIds { get; set; }

        public Guid UserId { get; set; }

        private Command(List<Guid> cryptoAnlysisHistoryIds, Guid userId)
        {
           CryptoAnlysisHistoryIds = cryptoAnlysisHistoryIds;
            UserId = userId;
        }

        public static Command Create(List<Guid> cryptoAnlysisHistoryIds, Guid userId)
        {
            return new Command(cryptoAnlysisHistoryIds,userId);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IReadOnlyRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> cryptoAnalysisHistoryRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {

            var user = _userRepo.Find(new UserWithCryptoCompare(request.UserId)).FirstOrDefault();

            List<Guid> validHistoryId = ExistHistoryRecordsIds(request.UserId,request.CryptoAnlysisHistoryIds);

            if (!validHistoryId.Any())
                return Task.FromResult<Result<bool, Error>>(Error.NotFound(nameof(AddCryptoAnylsisToCompare), nameof(CryptoComparison.CryptoAnalysisHistoryId), string.Join(",", request.CryptoAnlysisHistoryIds)));

            var existingHistoryIds = user!.Comparisons.Select(c=>c.CryptoAnalysisHistoryId).ToHashSet();
            var historyIdToAdd = validHistoryId.Where(id=>!existingHistoryIds.Contains(id)).ToList();

            if(!historyIdToAdd.Any())
                return Task.FromResult<Result<bool, Error>>(Error.ValueAlreadyExists(nameof(AddCryptoAnylsisToCompare), nameof(CryptoComparison.CryptoAnalysisHistoryId), string.Join(",", validHistoryId)));

            foreach (var historyId in historyIdToAdd)
            {
                var cryptoCompareCreationResult = CryptoComparison.Factory.Create(request.UserId, historyId);
                if(cryptoCompareCreationResult.IsFailure)
                    return Task.FromResult<Result<bool,Error>>(cryptoCompareCreationResult.Error);
                user.AddToCompareList(cryptoCompareCreationResult.Value);
            }

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private List<Guid> ExistHistoryRecordsIds(Guid userId, List<Guid> cryptoAnlysisHistoryIds) {

            List<Guid> validHistoryId = new List<Guid>();

            var userHistory = cryptoAnalysisHistoryRepo.GetAll().Where(h => h.UserId == userId);
            foreach (var historyId in cryptoAnlysisHistoryIds)
            {
                if (userHistory.Any(h => h.Id == historyId))
                    validHistoryId.Add(historyId);
            }

            return validHistoryId;
        }
    }
}
