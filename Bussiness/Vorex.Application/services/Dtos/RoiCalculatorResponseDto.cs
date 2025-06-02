namespace Vorex.Application.services.Dtos;

public record RoiCalculatorResponseDto
{
    public required decimal ReturnOfInvestment {  get; init; }
    public required decimal Roi { get; init; }
}
