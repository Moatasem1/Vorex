namespace Vorex.Application.Users.Contracts;

public class RefreshTokenResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime Expiration { get; set; }
}