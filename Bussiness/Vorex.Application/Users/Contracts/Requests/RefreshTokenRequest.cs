using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record RefreshTokenRequest
{
    public required string RefreshToken { get; set; }
}


public class RefreshTokenRequestValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenRequestValidator()
    {
        RuleFor(r => r.RefreshToken)
          .NotEmpty();
    }
}
