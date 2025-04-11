using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.Cryptos;
using Vorex.Domain.User;

namespace Vorex.Domain.CryptoAnalyses;

public class CryptoAnalysisHistory : BaseEntity, IAggregateRoot
{
    private CryptoAnalysisHistory() { }

    [Required]
    [ForeignKey(nameof(User.User))]
    public Guid UserId { get; private set; }

    [Required]
    [ForeignKey(nameof(Crypto))]
    public Guid CryptoId { get; private set; }

    [Required]
    [Range(0, double.MaxValue)]  
    public decimal Amount { get; private set; }

    [Required]
    [Range(1, int.MaxValue)] 
    public int HoldingDays { get; private set; }

    [Required]
    public DateTime SubmitDate { get; private set; }

    [Required]
    [Range(1, float.MaxValue)]
    public float Risk { get; private set; }

    private readonly List<CryptoFavorite> _favorites = [];
    public virtual IReadOnlyCollection<CryptoFavorite> Favorites => _favorites.AsReadOnly();

    private readonly List<CryptoComparison> _comparisons = [];
    public  virtual IReadOnlyCollection<CryptoComparison> Comparisons => _comparisons.AsReadOnly();
}