namespace Vorex.Application.Users.Contracts;

public class LoginResponse
{
    public required string Token { get; set; }
    public required string RefreshToken { get; set; }
    public required DateTime Expiration { get; set; }
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
}
