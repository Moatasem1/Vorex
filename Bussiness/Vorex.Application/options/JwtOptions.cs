namespace Vorex.Application.options;

public class JwtOptions
{
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public required string SigningKey { get; set; }
}
