using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.lib;
using Vorex.Domain.lib.Interfaces;

namespace Vorex.Domain.User;
public class CryptoFavorite : IEntity
{ 
    private CryptoFavorite() { }

    public Guid UserId { get; private set; }

    public Guid CryptoAnalysisHistoryId { get; private set; }

    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;

    public class Factory
    {
        public static Result<CryptoFavorite, Error> Create(Guid userId, Guid cryptoAnalysisHistoryId)
        {
            var userIdValidation = ValidateId(userId, nameof(UserId));
            if (userIdValidation.IsFailure) return userIdValidation.Error;

            var cryptoIdValidation = ValidateId(cryptoAnalysisHistoryId, nameof(CryptoAnalysisHistoryId));
            if (cryptoIdValidation.IsFailure) return cryptoIdValidation.Error;

            var cryptoFavorite = new CryptoFavorite
            {
                UserId = userId,
                CryptoAnalysisHistoryId = cryptoAnalysisHistoryId,
                CreatedAt = DateTime.UtcNow,
            };

            return cryptoFavorite;
        }
    }

    //validation methods
    private static Result<bool, Error> ValidateId(Guid id,string propertyName = nameof(UserId))
    {
        return id == Guid.Empty ? Error.ValueRequired(nameof(CryptoFavorite), propertyName) : true;
    }

}
