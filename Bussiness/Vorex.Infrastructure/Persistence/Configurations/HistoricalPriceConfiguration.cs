using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.Cryptos;

namespace Vorex.Infrastructure.Persistence.Configurations;

public class HistoricalPriceConfiguration : IEntityTypeConfiguration<HistoricalPrice>
{
    public void Configure(EntityTypeBuilder<HistoricalPrice> builder)
    {
        builder.HasKey(hp => hp.Id);

        builder.Property(hp => hp.Year)
          .IsRequired();

        builder.Property(hp => hp.Month)
           .IsRequired();

        builder.Metadata.AddCheckConstraint(
        "CK_HistoricalPrice_Year_Valid",
        "[Year] >= 1 AND [Year] <= 9999"
        );

        builder.Metadata.AddCheckConstraint(
            "CK_HistoricalPrice_Month_Valid",
            "[Month] >= 1 AND [Month] <= 12"
        );


        builder.Property(hp => hp.ClosingPrice)
            .IsRequired()
            .HasPrecision(18, 18);

        builder.Property(hp => hp.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
    }
}