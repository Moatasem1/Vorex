using FluentValidation;

namespace Vorex.Application.Cryptos.Contracts.Request;

public record AnalyzeCryptoRiskRequest
{
    public required decimal InvestmentAmount { get; init; }
    public required int HoldingDays { get; init; }
}

public class AnalyzeCryptoRiskRequestValidator : AbstractValidator<AnalyzeCryptoRiskRequest>
{
    public AnalyzeCryptoRiskRequestValidator()
    {
        RuleFor(x => x.InvestmentAmount)
            .NotEmpty()
            .GreaterThan(0);
        RuleFor(x => x.HoldingDays)
            .NotEmpty()
            .GreaterThan(0);
    }
}