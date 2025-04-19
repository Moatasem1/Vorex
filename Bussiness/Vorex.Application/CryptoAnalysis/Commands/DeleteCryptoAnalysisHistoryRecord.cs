using MediatR;
using Vorex.Application.Users.Commands;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User;

namespace Vorex.Application.CryptoAnalysis.Commands;

public class DeleteCryptoAnalysisHistoryRecord
{
    public sealed class Command : IRequest<Result<bool, Error>>
    {
        public  Guid UserId { get; private set; }
        public  Guid CryptoAnalysisHistoryRecordId { get; private set; }

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

    public sealed class Handler(IRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> _cryptoAnalysisHistoryRepo) : IRequestHandler<Command, Result<bool, Error>>
    {
        public Task<Result<bool, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);
            if (canHandle.IsFailure)
                return Task.FromResult<Result<bool, Error>>(canHandle.Error);

            var cryptoAnalysisHistoryRecord = _cryptoAnalysisHistoryRepo.GetAll().FirstOrDefault(x => x.Id == request.CryptoAnalysisHistoryRecordId && x.UserId == request.UserId);

            _cryptoAnalysisHistoryRepo.Remove(cryptoAnalysisHistoryRecord!);

            return Task.FromResult<Result<bool, Error>>(true);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var isCryptoAnylsisRecordIdFound = _cryptoAnalysisHistoryRepo.GetAll().Any(x => x.Id == command.CryptoAnalysisHistoryRecordId && x.UserId==command.UserId);

            if (!isCryptoAnylsisRecordIdFound)
                return Error.NotFound(nameof(DeleteCryptoAnalysisHistoryRecord),nameof(CryptoAnalysisHistory.Id) , command.CryptoAnalysisHistoryRecordId.ToString());

            return true;
        }
    }
}
