using System;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib;
using Vorex.Domain.User;

namespace Vorex.Domain.CryptoAnalyses;

public class CryptoAnalysisHistory : BaseEntity, IAggregateRoot
{
    private CryptoAnalysisHistory() { }

    public Guid UserId { get; private set; }

    public Guid CryptoId { get; private set;}

    public decimal Amount { get; private set; }

    public int HoldingDays { get; private set; }

    public decimal Risk { get; private set; }

    private readonly List<CryptoComparison> _comparisons = new();
    public IReadOnlyCollection<CryptoComparison> Comparisons => _comparisons.AsReadOnly();

    public static class Factory
    {
        public static Result<CryptoAnalysisHistory, Error> Create(Guid userId, Guid cryptoId, decimal amount, int holdingDays, DateTime submitDate, decimal risk)
        {
            var userIdValidation = ValidateId(userId,nameof(Id));
            if (userIdValidation.IsFailure) return userIdValidation.Error;

            var cryptoIdValidation = ValidateId(userId, nameof(userId));
            if (cryptoIdValidation.IsFailure) return cryptoIdValidation.Error;

            var amountValidation = ValidateAmount(amount);
            if (amountValidation.IsFailure) return amountValidation.Error;

            var holdingDaysValidation = ValidateHoldingDays(holdingDays);
            if (holdingDaysValidation.IsFailure) return holdingDaysValidation.Error;

            var riskValidation = ValidateRisk(risk);
            if (riskValidation.IsFailure) return riskValidation.Error;
                
            var cryptoAnalysisHistory = new CryptoAnalysisHistory
            {
                Id = Guid.NewGuid(),
                CreatedAt = DateTime.UtcNow,
                UserId = userId,
                CryptoId = cryptoId,
                Amount = amount,
                HoldingDays = holdingDays,
                Risk = Math.Round(risk, 2),
            };

            return cryptoAnalysisHistory;
        }
    }

    // Validation methods
    private static Result<bool, Error> ValidateId(Guid id,string propertyName = nameof(Id))
    {
        return id == Guid.Empty ? Error.ValueRequired(nameof(CryptoAnalysisHistory), propertyName) : true;
    }
    private static Result<bool, Error> ValidateAmount(decimal amount)
    {
        return amount <= 0 ? Error.ValueRequired(nameof(CryptoAnalysisHistory), nameof(Amount)) : true;
    }
    private static Result<bool, Error> ValidateHoldingDays(int holdingDays)
    {
        return holdingDays <= 0 ? Error.ValueRequired(nameof(CryptoAnalysisHistory), nameof(HoldingDays)) : true;
    }
    private static Result<bool, Error> ValidateRisk(decimal risk)
    {
        return risk <= 0 ? Error.ValueRequired(nameof(CryptoAnalysisHistory), nameof(Risk)) : true;
    }
}