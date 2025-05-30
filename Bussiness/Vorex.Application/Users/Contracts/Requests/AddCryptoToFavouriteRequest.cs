using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record AddCryptoToFavouriteRequest
{
    public Guid CryptoId { get; set; }
}

public class AddCryptoToFavouriteRequestValidator : AbstractValidator<AddCryptoToFavouriteRequest>
{
    public AddCryptoToFavouriteRequestValidator()
    {
        RuleFor(x => x.CryptoId).NotEmpty();
    }
}