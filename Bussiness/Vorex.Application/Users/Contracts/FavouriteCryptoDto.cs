namespace Vorex.Application.Users.Contracts;

public record FavouriteCryptoDto
{
    public Guid Id { get; init; }
    public required string Name { get; init; }
    public required string Symbol { get; init; }
}