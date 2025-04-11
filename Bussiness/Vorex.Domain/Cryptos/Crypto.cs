using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;

namespace Vorex.Domain.Cryptos;

class Crypto : BaseEntity, IAggregateRoot
{
    private Crypto(string name, string symbol, int volatilityLevelId)
    {
        Name = name;
        Symbol = symbol;
        VolatilityLevelID = volatilityLevelId;
    }

    [Required]
    [StringLength(100)]
    public string Name { get; private set; }

    [Required]
    [StringLength(10)]
    public string Symbol { get; private set; }

    [Required]
    public int VolatilityLevelID { get; private set; }

    [ForeignKey(nameof(VolatilityLevelID))]
    public virtual VolatilityLevel VolatilityLevel { get; private set; } = new VolatilityLevel();

    private readonly List<HistoricalPrice> _historicalPrices = [];
    public virtual IReadOnlyCollection<HistoricalPrice> HistoricalPrices => _historicalPrices.AsReadOnly();
}