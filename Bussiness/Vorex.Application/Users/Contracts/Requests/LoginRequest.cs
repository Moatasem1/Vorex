using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Vorex.Application.Users.Contracts.Requests;

public class LoginRequest
{
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty()
        .MaximumLength(20);
    }
}