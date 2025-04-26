using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Cryptos;
using Vorex.Domain.User;

namespace Vorex.Infrastructure.Persistence.Configurations;

public class CryptoAnalysisHistoryConfiguration : IEntityTypeConfiguration<CryptoAnalysisHistory>
{
    public void Configure(EntityTypeBuilder<CryptoAnalysisHistory> builder)
    {
        builder.HasKey(c => c.Id);

        builder.Property(c => c.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.CryptoId)
            .IsRequired();

        builder.Property(c => c.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(c => c.HoldingDays)
            .IsRequired();

        builder.Property(c => c.Risk)
            .HasPrecision(18, 18)
            .IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Crypto>()
            .WithMany()
            .HasForeignKey(x => x.CryptoId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany<CryptoFavorite>()
           .WithOne()
           .HasForeignKey(x => x.CryptoAnalysisHistoryId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.HasMany<CryptoComparison>()
           .WithOne()
          .HasForeignKey(x => x.CryptoAnalysisHistoryId)
           .OnDelete(DeleteBehavior.NoAction);

        builder.Metadata.AddCheckConstraint("CK_CryptoAnalysisHistory_Amount_Positive", "[Amount] > 0");
        builder.Metadata.AddCheckConstraint("CK_CryptoAnalysisHistory_HoldingDays_Positive", "[HoldingDays] > 0");
        builder.Metadata.AddCheckConstraint("CK_CryptoAnalysisHistory_Risk_Positive", "[Risk] > 0");
    }
}