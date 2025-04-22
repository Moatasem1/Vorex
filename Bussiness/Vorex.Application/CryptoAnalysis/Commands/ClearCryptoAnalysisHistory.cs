using MediatR;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.CryptoAnalysis.Commands;

public class ClearCryptoAnalysisHistory
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public Guid UserId { get; private set; }

        private Command(Guid userId)
        {
            UserId = userId;
        }

        public static Command Create(Guid userId)
        {
            return new Command(userId);
        }
    }

    public sealed class Handler(IRepository<Domain.User.User> _userRepo, IRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> _cryptoAnalysisHistoryRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandle.Error);

            var cryptoAnalysisHistoryRecords = _cryptoAnalysisHistoryRepo.GetAll().Where(x => x.UserId == request.UserId);
            var user = _userRepo.Find(new UserWithCryptoFavoriteAndCompareList(request.UserId)).FirstOrDefault();

            foreach(var cryptoAnalysisHistoryRecord in cryptoAnalysisHistoryRecords)
            {
                //user!.RemoveFromFavorites(cryptoAnalysisHistoryRecord.Id);
                //user.RemoveFromCompareList(cryptoAnalysisHistoryRecord.Id);
                _cryptoAnalysisHistoryRepo.Remove(cryptoAnalysisHistoryRecord);
            }

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            return true;
        }
    }
}
