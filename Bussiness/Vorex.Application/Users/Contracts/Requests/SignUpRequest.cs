using FluentValidation;
using System.ComponentModel.DataAnnotations;

namespace Vorex.Application.Users.Contracts.Requests;

public class SignUpRequest
{
    public required string FirstName { get; set; }
    public required string LastName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class SignUpRequestValidator : AbstractValidator<SignUpRequest>
{
    public SignUpRequestValidator()
    {
        RuleFor(x => x.FirstName)
           .NotEmpty()
           .MaximumLength(50);

        RuleFor(x => x.LastName)
          .NotEmpty()
          .MaximumLength(50);

        RuleFor(x => x.Email)
        .NotEmpty()
        .EmailAddress();

        RuleFor(x => x.Password)
        .NotEmpty()
        .MaximumLength(20);
    }
}