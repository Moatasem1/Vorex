using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Domain.Cryptos;

public class HistoricalPrice : BaseEntity, IEntity
{
    private HistoricalPrice() { }

    public DateOnly Date {  get; private set; }
    
    public decimal ClosingPrice { get; private set; }

    public Guid CryptoId { get; private set; }

    public static class Factory
    {
        public static Result<HistoricalPrice, Error> Create(Guid cryptoId, DateOnly date, decimal closingPrice)
        {
            var idValidation = ValidateCryptoId(cryptoId);
            if (idValidation.IsFailure) return idValidation.Error;

            var priceValidation = ValidateClosingPrice(closingPrice);
            if (priceValidation.IsFailure) return priceValidation.Error;

            var price = new HistoricalPrice
            {
                Id = Guid.NewGuid(),
                CryptoId = cryptoId,
                Date = date,
                ClosingPrice = closingPrice,
                CreatedAt = DateTime.UtcNow
            };

            return price;
        }
    }

    // Validation Methods
    private static Result<bool, Error> ValidateCryptoId(Guid id)
    {
        return id == Guid.Empty
            ? Error.ValueRequired(nameof(HistoricalPrice), nameof(CryptoId))
            : true;
    }

    private static Result<bool, Error> ValidateClosingPrice(decimal price)
    {
        return price < 0
            ? Error.ValueInvalid(nameof(HistoricalPrice), nameof(ClosingPrice))
            : true;
    }
}