namespace Vorex.Application.Cryptos.Contracts;

public record CryptoHistoricalPriceDto
{
    public Guid Id { get; set; }
    public DateOnly Date { get; set; }
    public decimal ClosingPrice { get; set; }

}