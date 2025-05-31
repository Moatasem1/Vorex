using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.Cryptos;

namespace Vorex.Infrastructure.Persistence.Configurations;

public class HistoricalPriceConfiguration : IEntityTypeConfiguration<HistoricalPrice>
{
    public void Configure(EntityTypeBuilder<HistoricalPrice> builder)
    {
        builder.HasKey(hp => hp.Id);

        builder.Property(hp => hp.Date)
           .IsRequired();

        builder.Property(hp => hp.ClosingPrice)
            .IsRequired()
            .HasPrecision(38, 31);

        builder.Property(hp => hp.CreatedAt)
            .IsRequired()
            .HasDefaultValueSql("GETDATE()");
    }
}