using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record ResetPasswordRequest
{
    public required string Token { get; init; }
    public required string NewPassword { get; init; }
}

public class ResetPasswordRequestValidator: AbstractValidator<ResetPasswordRequest>
{
    public ResetPasswordRequestValidator()
    {
        RuleFor(r => r.Token)
        .NotEmpty();

        RuleFor(r => r.NewPassword)
        .NotEmpty()
        .MinimumLength(4)
        .MaximumLength(20);
    }
}