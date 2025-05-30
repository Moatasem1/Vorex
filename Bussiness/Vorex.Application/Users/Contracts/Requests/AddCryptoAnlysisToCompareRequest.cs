using FluentValidation;

namespace Vorex.Application.Users.Contracts.Requests;

public record AddCryptoAnlysisToCompareRequest
{
    public required List<Guid> CryptoAnlysisHistoryIds { get; init; }

}

public class AddCryptoAnlysisToCompareRequestValidator : AbstractValidator<AddCryptoAnlysisToCompareRequest>
{
    public AddCryptoAnlysisToCompareRequestValidator() {
        RuleFor(h => h.CryptoAnlysisHistoryIds).NotEmpty();
        RuleForEach(h=>h.CryptoAnlysisHistoryIds).NotEmpty();
    }
}