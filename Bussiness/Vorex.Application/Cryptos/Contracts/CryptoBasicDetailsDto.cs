namespace Vorex.Application.Cryptos.Contracts;

public record CryptoBasicDetailsDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }

    public required string Symbol { get; init; }

    public required bool IsFavourite { get; init; }

    public required int VoltiltlyLevelId { get; init; }
}
