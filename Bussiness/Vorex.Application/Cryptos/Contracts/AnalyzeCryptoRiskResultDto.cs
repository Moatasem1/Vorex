namespace Vorex.Application.Cryptos.Contracts;

public record AnalyzeCryptoRiskResultDto
{
    public required Guid CryptoAnalysisHistoryId { get; init; }
    public required decimal RiskValue { get; init; }

    public required decimal ReturnOfInvestment { get; init; }
}