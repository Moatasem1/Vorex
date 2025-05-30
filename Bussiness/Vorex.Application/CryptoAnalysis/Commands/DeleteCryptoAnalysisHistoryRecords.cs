using MediatR;
using Vorex.Application.Users.Commands;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.CryptoAnalysis.Commands;

public class DeleteCryptoAnalysisHistoryRecords
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public  Guid UserId { get; private set; }
        public List<Guid> CryptoAnalysisHistoryRecordIds { get; private set; }

      private Command(Guid userId, List<Guid> cryptoAnalysisHistoryRecordId)
        {
            UserId = userId;
            CryptoAnalysisHistoryRecordIds = cryptoAnalysisHistoryRecordId;
        }

       public static Command Create(Guid userId, List<Guid> cryptoAnalysisHistoryRecordId)
        {
            return new Command(userId, cryptoAnalysisHistoryRecordId);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> _cryptoAnalysisHistoryRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandle.Error);

            var cryptoAnalysisHistoryEntries = _cryptoAnalysisHistoryRepo.GetAll().Where(r => request.CryptoAnalysisHistoryRecordIds.Contains(r.Id) && r.UserId == request.UserId);

            var user = _userRepo.Find(new UserWithCryptoFavoriteAndCompareList(request.UserId)).FirstOrDefault();

            var historyRecordsIdsToRemoveInCompareList = user!.Comparisons.Where(r=>request.CryptoAnalysisHistoryRecordIds.Contains(r.CryptoAnalysisHistoryId)).Select(r=>r.CryptoAnalysisHistoryId).ToList();
            var compareListRemoveResult = user!.RemoveFromCompareList(historyRecordsIdsToRemoveInCompareList);

            if(compareListRemoveResult.IsFailure)
                return Task.FromResult<Result<bool, Error>>(compareListRemoveResult);

            _cryptoAnalysisHistoryRepo.RemoveRange(cryptoAnalysisHistoryEntries);

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var validIds = command.CryptoAnalysisHistoryRecordIds.Where(id => _cryptoAnalysisHistoryRepo.GetAll().Any(r => r.Id == id && r.UserId == command.UserId)).ToList();
            var invalidIds = command.CryptoAnalysisHistoryRecordIds.Except(validIds).ToList();

            if (invalidIds.Count()>0)
                return Error.NotFound(nameof(DeleteCryptoAnalysisHistoryRecords),nameof(CryptoAnalysisHistory.Id) , String.Join(",", invalidIds));

            return true;
        }
    }
}
