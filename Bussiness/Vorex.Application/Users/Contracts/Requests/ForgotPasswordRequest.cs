using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record ForgotPasswordRequest
{
    public required string Email { get; init; } 
}

public class ForgotPasswordRequestValidator : AbstractValidator<ForgotPasswordRequest>
{
    public ForgotPasswordRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}