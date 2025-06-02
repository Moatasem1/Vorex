namespace Vorex.Application.services.Dtos;

public record RoiCalculatorInput
{
    public required string CryptoIdentity {  get; init; }
    public required decimal InvestAmount { get; init; }
}
