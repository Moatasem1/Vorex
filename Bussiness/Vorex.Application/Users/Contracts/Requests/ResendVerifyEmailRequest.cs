using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record ResendVerifyEmailRequest
{
    public required string Email { set; get; }
}

public class ResendVerifyEmailRequestValidator : AbstractValidator<ResendVerifyEmailRequest>
{
    public ResendVerifyEmailRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}
