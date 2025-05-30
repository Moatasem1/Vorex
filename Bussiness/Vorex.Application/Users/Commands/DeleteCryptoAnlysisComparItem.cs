using MediatR;
using Vorex.Application.CryptoAnalysis.Commands;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User;
using Vorex.Domain.User.Specifications;

namespace Vorex.Application.Users.Commands;

public class DeleteCryptoAnlysisComparItem
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public Guid UserId { get; private set; }
        public Guid CryptoAnalysisHistoryRecordId { get; private set; }

        private Command(Guid userId, Guid cryptoAnalysisHistoryRecordId)
        {
            UserId = userId;
            CryptoAnalysisHistoryRecordId = cryptoAnalysisHistoryRecordId;
        }

        public static Command Create(Guid userId, Guid cryptoAnalysisHistoryRecordId)
        {
            return new Command(userId, cryptoAnalysisHistoryRecordId);
        }
    }

    public sealed class Handler(IReadOnlyRepository<Domain.User.User> userReadOnlyRepo, IRepository<Domain.User.User> _userRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandle.Error);

            var user = _userRepo.Find(new UserWithCryptoCompare(request.UserId)).FirstOrDefault();

            var removeResult =  user!.RemoveFromCompareList([request.CryptoAnalysisHistoryRecordId]);

            if (removeResult.IsFailure) 
                   return Task.FromResult<Result<bool,Error>>(removeResult.Error);
            

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var user = userReadOnlyRepo.Find(new UserWithCryptoCompare(command.UserId)).FirstOrDefault();  

            if (!user!.Comparisons.Any(c=>c.CryptoAnalysisHistoryId==command.CryptoAnalysisHistoryRecordId))
                return Error.NotFound(nameof(DeleteCryptoAnlysisComparItem), nameof(CryptoComparison.CryptoAnalysisHistoryId), command.CryptoAnalysisHistoryRecordId.ToString());

            return true;
        }
    }
}
