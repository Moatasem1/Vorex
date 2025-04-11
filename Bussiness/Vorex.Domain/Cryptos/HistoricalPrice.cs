using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common.Interfaces;

namespace Vorex.Domain.Cryptos;

class HistoricalPrice : IEntity
{
    private HistoricalPrice() { }

    [Required]
    [Range(1, short.MaxValue)]
    public int Year { get; private set; }

    [Required]
    [Range(1, 12)]
    public int Month { get; private set; }

    [Required]
    [Range(0, double.MaxValue)] 
    public decimal ClosingPrice { get; private set; }

    [Required]
    [ForeignKey(nameof(Crypto))]
    public Guid CryptoId { get; private set; }
}
