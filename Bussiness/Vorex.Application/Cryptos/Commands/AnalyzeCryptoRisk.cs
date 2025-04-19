using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Application.Users.Commands;
using Vorex.Domain.Cryptos;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Cryptos.Commands;

public class AnalyzeCryptoRisk
{
    public sealed class Command : IRequest<Result<AnalyzeCryptoRiskResultDto, Error>>
    {
        public Guid UserId { get; private set; }
        public Guid CryptoId { get; private set; }
        public decimal InvestmentAmount { get; private set; }
        public int HoldingDays { get; private set; }

        private Command(Guid userId,Guid cryptoId, decimal investmentAmount, int holdingDays)
        {
            CryptoId = cryptoId;
            InvestmentAmount = investmentAmount;
            HoldingDays = holdingDays;
            UserId = userId;
        }

        public static Command Create(Guid userId, Guid cryptoId, decimal investmentAmount, int holdingDays)
        {
            return new Command(userId,cryptoId, investmentAmount, holdingDays);
        }
    }

    public sealed class Handler(IRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> _cryptoAnalysisHistoryRepository, IRepository<Domain.Cryptos.Crypto> _cryptoRepository) : IRequestHandler<Command, Result<AnalyzeCryptoRiskResultDto, Error>>
    {
        public Task<Result<AnalyzeCryptoRiskResultDto, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);

            if (canHandle.IsFailure)
                return Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(canHandle.Error);

            decimal riskValue = 0.03M; // Placeholder for actual risk calculation logic

            var analysisHistory = Domain.CryptoAnalyses.CryptoAnalysisHistory.Factory.Create(request.UserId,request.CryptoId, request.InvestmentAmount,  request.HoldingDays,DateTime.Now, riskValue);
            if (analysisHistory.IsFailure)
                return Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(analysisHistory.Error);

            _cryptoAnalysisHistoryRepository.Add(analysisHistory.Value);

            var result = new AnalyzeCryptoRiskResultDto
            {
                RiskValue = riskValue,
                CryptoAnalysisHistoryId = analysisHistory.Value.Id,
            };

            return Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(result);
        }

        private Result<bool, Error> CanHandle(Command command)
        {
            var isCryptoIdExsist = _cryptoRepository.GetAll().Any(x => x.Id == command.CryptoId);

            if (!isCryptoIdExsist)
                return Error.NotFound(nameof(CreateUser), nameof(Crypto.Id), command.CryptoId.ToString());

            return true;
        }
    }
}
