namespace Vorex.Application.Cryptos.Contracts;

public record CryptoHistoricalPriceDto
{
    public DateOnly MinDate { get; set; }
    public DateOnly MaxDate { get; set; }
    public DateOnly StartDate { get; set; }
    public DateOnly EndDate { get; set; }
    public required List<CryptoHistoricalPriceItemDto> Data { get; set; }
}

public record CryptoHistoricalPriceItemDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal ClosingPrice { get; set; }
}