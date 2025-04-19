namespace Vorex.Application.Cryptos.Contracts;

public record AnalyzeCryptoRiskResultDto
{
    public Guid CryptoAnalysisHistoryId { get; init; }
    public decimal RiskValue { get; init; }
}