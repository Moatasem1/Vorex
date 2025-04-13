using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Domain.Cryptos;

public class HistoricalPrice : BaseEntity, IEntity
{
    private HistoricalPrice() { }

    public int Year { get; private set; }

    public int Month { get; private set; }

    public decimal ClosingPrice { get; private set; }

    public Guid CryptoId { get; private set; }

    public static class Factory
    {
        public static Result<HistoricalPrice, Error> Create(Guid cryptoId, int year, int month, decimal closingPrice)
        {
            var idValidation = ValidateCryptoId(cryptoId);
            if (idValidation.IsFailure) return idValidation.Error;

            var yearValidation = ValidateYear(year);
            if (yearValidation.IsFailure) return yearValidation.Error;

            var monthValidation = ValidateMonth(month);
            if (monthValidation.IsFailure) return monthValidation.Error;

            var priceValidation = ValidateClosingPrice(closingPrice);
            if (priceValidation.IsFailure) return priceValidation.Error;

            var price = new HistoricalPrice
            {
                Id = Guid.NewGuid(),
                CryptoId = cryptoId,
                Year = year,
                Month = month,
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

    private static Result<bool, Error> ValidateYear(int year)
    {
        return year is < 1 or > short.MaxValue
            ? Error.ValueInvalid(nameof(HistoricalPrice), nameof(Year))
            : true;
    }

    private static Result<bool, Error> ValidateMonth(int month)
    {
        return month is < 1 or > 12
            ? Error.ValueInvalid(nameof(HistoricalPrice), nameof(Month))
            : true;
    }

    private static Result<bool, Error> ValidateClosingPrice(decimal price)
    {
        return price < 0
            ? Error.ValueInvalid(nameof(HistoricalPrice), nameof(ClosingPrice))
            : true;
    }
}