using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.lib;

namespace Vorex.Domain.Cryptos;

public class Crypto : BaseEntity, IAggregateRoot
{
    private Crypto() { }
    private Crypto(string name, string symbol, int volatilityLevelId)
    {
        Id = Guid.NewGuid();
        CreatedAt = DateTime.UtcNow;
        Name = name;
        Symbol = symbol;
        VolatilityLevelID = volatilityLevelId;
    }

    public string Name { get; private set; }

    public string Symbol { get; private set; }

    public int VolatilityLevelID { get; private set; }

    public virtual VolatilityLevel VolatilityLevel { get; private set; } = new VolatilityLevel();

    private readonly List<HistoricalPrice> _historicalPrices = [];
    public virtual IReadOnlyCollection<HistoricalPrice> HistoricalPrices => _historicalPrices.AsReadOnly();

    public static class Factory
    {
        public static Result<Crypto, Error> Create(string name, string symbol, int volatilityLevelId)
        {
            var nameValidation = ValidateName(name);
            if (nameValidation.IsFailure) return nameValidation.Error;

            var symbolValidation = ValidateSymbol(symbol);
            if (symbolValidation.IsFailure) return symbolValidation.Error;

            var volatilityValidation = ValidateVolatilityLevelId(volatilityLevelId);
            if (volatilityValidation.IsFailure) return volatilityValidation.Error;

            var crypto = new Crypto(name, symbol, volatilityLevelId);
            return crypto;
        }
    }

    //validation methods
    private static Result<bool, Error> ValidateName(string name)
    {
        return string.IsNullOrWhiteSpace(name)
            ? Error.ValueRequired(nameof(Crypto), nameof(Name))
            : name.Length > 100
                ? Error.ValueInvalid(nameof(Crypto), nameof(Name))
                : true;
    }

    private static Result<bool, Error> ValidateSymbol(string symbol)
    {
        return string.IsNullOrWhiteSpace(symbol)
            ? Error.ValueRequired(nameof(Crypto), nameof(Symbol))
            : symbol.Length > 10
                ? Error.ValueInvalid(nameof(Crypto), nameof(Symbol))
                : true;
    }

    private static Result<bool, Error> ValidateVolatilityLevelId(int id)
    {
         return Enumeration.GetAll<VolatilityLevel>().Any(v => v.Id == id)
            ? true
            : Error.ValueInvalid(nameof(Crypto), nameof(VolatilityLevelID));
    }
}