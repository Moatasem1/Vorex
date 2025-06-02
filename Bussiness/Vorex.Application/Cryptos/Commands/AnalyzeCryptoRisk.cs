using MediatR;
using Vorex.Application.Cryptos.Contracts;
using Vorex.Application.services.Dtos;
using Vorex.Application.services.interfaces;
using Vorex.Application.Users.Commands;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Cryptos;
using Vorex.Domain.Cryptos.Specifications;
using Vorex.Domain.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Application.Cryptos.Commands;

public class AnalyzeCryptoRisk
{
    public sealed class Command : IRequest<Result<AnalyzeCryptoRiskResultDto, Error>>
    {
        public Guid? UserId { get; private set; }
        public Guid CryptoId { get; private set; }
        public decimal InvestmentAmount { get; private set; }

        private Command(Guid? userId,Guid cryptoId, decimal investmentAmount)
        {
            CryptoId = cryptoId;
            InvestmentAmount = investmentAmount;
            UserId = userId;
        }

        public static Command Create(Guid? userId, Guid cryptoId, decimal investmentAmount)
        {
            return new Command(userId,cryptoId, investmentAmount);
        }
    }

    public sealed class Handler(IRepository<Domain.CryptoAnalyses.CryptoAnalysisHistory> _cryptoAnalysisHistoryRepository, IReadOnlyRepository<Domain.Cryptos.Crypto> _cryptoRepository, IRoiCalculator roiCalculator) : IRequestHandler<Command, Result<AnalyzeCryptoRiskResultDto, Error>>
    {
        public async Task<Result<AnalyzeCryptoRiskResultDto, Error>> Handle(Command request, CancellationToken cancellationToken)
        {
            var canHandle = CanHandle(request);

            if (canHandle.IsFailure)
                return await Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(canHandle.Error);

            var crypto = _cryptoRepository.Find(new CryptoSpecification(request.CryptoId)).FirstOrDefault();

            //var anlyzeInput = new RoiCalculatorInput { CryptoIdentity = $"{crypto!.Name}_{crypto!.Symbol}", InvestAmount = request.InvestmentAmount };
            //var anlysisResp = await roiCalculator.CalculateRoi(anlyzeInput);

            //if (anlysisResp.IsFailure)
            //    return await Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(anlysisResp.Error);

            var anlysisResp = new RoiCalculatorResponseDto
            {
                ReturnOfInvestment = request.InvestmentAmount+ ( request.InvestmentAmount * .03M),
                Roi = .03M
            };


            var isUserFound = request.UserId.HasValue;

            Result<CryptoAnalysisHistory, Error>? analysisHistory= null;
            if (isUserFound)
            {
                analysisHistory = Domain.CryptoAnalyses.CryptoAnalysisHistory.Factory.Create(request.UserId!.Value, request.CryptoId, request.InvestmentAmount, 30, DateTime.Now, anlysisResp.Roi);
                if (analysisHistory.IsFailure)
                    return await Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(analysisHistory.Error);

                _cryptoAnalysisHistoryRepository.Add(analysisHistory.Value);
            }

            var result = new AnalyzeCryptoRiskResultDto
            {
                RiskValue = anlysisResp.Roi,
                CryptoAnalysisHistoryId = isUserFound ? analysisHistory!.Value.Id : Guid.Empty,
                ReturnOfInvestment = anlysisResp.ReturnOfInvestment
            };

            return await Task.FromResult<Result<AnalyzeCryptoRiskResultDto, Error>>(result);
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
