using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Vorex.Domain.Common;
using Vorex.Domain.Common.Interfaces;
using Vorex.Domain.CryptoAnalyses;

namespace Vorex.Domain.User;
//don't forget to make userId and CryptoAnalysisHistoryId prmary key
public class CryptoComparison : IEntity
{
    private CryptoComparison() { }

    [Required]
    [ForeignKey(nameof(User))]
    public Guid UserId { get; private set; }

    [Required]
    [ForeignKey(nameof(CryptoAnalysisHistory))]
    public Guid CryptoAnalysisHistoryId { get; private set; }

    [Required]
    public DateTime CreatedAt { get; protected set; } = DateTime.UtcNow;
}
