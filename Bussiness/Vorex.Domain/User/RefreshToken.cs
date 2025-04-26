using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Cryptos;
using Vorex.Domain.lib;

namespace Vorex.Domain.User;

public class RefreshToken : BaseEntity, IEntity
{
    private RefreshToken() { }
    public string Token { get; private set; }
    public Guid UserId { get; private set; }
    public DateTime ExpiresOn { get; private set; }
    public bool IsExpired => DateTime.UtcNow >= ExpiresOn;

    public static class Factory
    {
        public static Result<RefreshToken, Error> Create(Guid userId, string token, DateTime expiresOn)
        {
            var tokenValidation = ValidateToken(token);
            if (tokenValidation.IsFailure)
                return tokenValidation.Error;

            var expireDateValidation = ValidateExpireDate(expiresOn);
            if (expireDateValidation.IsFailure)
                return expireDateValidation.Error;

            return new RefreshToken
            {
                UserId = userId,
                Token = token,
                ExpiresOn = expiresOn,
                CreatedAt = DateTime.UtcNow
            };
        }
    }

    //validations
    private static Result<bool, Error> ValidateToken(string token)
    {
        return string.IsNullOrEmpty(token)
            ? Error.ValueRequired(nameof(RefreshToken), nameof(RefreshToken.Token))
            : true;
    }

    private static Result<bool, Error> ValidateExpireDate(DateTime expiresOn)
    {
        return expiresOn < DateTime.UtcNow
            ? Error.ValueRequired(nameof(RefreshToken), nameof(RefreshToken.Token))
            : true;
    }
}
