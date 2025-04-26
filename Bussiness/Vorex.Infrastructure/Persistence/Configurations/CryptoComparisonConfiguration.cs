using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.User;

namespace Vorex.Infrastructure.Persistence.Configurations;

public class CryptoComparisonConfiguration : IEntityTypeConfiguration<CryptoComparison>
{
    public void Configure(EntityTypeBuilder<CryptoComparison> builder)
    {
        builder.HasKey(cc => new { cc.UserId, cc.CryptoAnalysisHistoryId }); 

        builder.Property(cc => cc.UserId)
            .IsRequired();

        builder.Property(cc => cc.CryptoAnalysisHistoryId)
            .IsRequired();

        builder.Property(cc => cc.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");  
    }
}
