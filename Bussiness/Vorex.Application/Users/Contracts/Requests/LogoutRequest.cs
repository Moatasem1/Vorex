using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record LogoutRequest
{
    public required string RefreshToken { get; init; }
}

public class LogoutRequestValidator: AbstractValidator<LogoutRequest>
{
    public LogoutRequestValidator()
    {
        RuleFor(l => l.RefreshToken)
        .NotEmpty();
    }
}
