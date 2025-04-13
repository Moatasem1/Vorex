using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.Cryptos;

namespace Vorex.Infrastructure.Persistence.Configurations;

class CryptoConfiguration : IEntityTypeConfiguration<Crypto>
{
    public void Configure(EntityTypeBuilder<Crypto> builder)
    {
       builder.HasKey(c => c.Id);

       builder.Property(c => c.Name)
             .IsRequired()
             .HasMaxLength(100);
             

        builder.Property(c => c.Symbol)
                .IsRequired()
                .HasMaxLength(10);

        builder.Property(c => c.VolatilityLevelID)
            .IsRequired();

        //relationships
        builder.HasOne(c => c.VolatilityLevel)
            .WithMany()
            .HasForeignKey(c => c.VolatilityLevelID)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(c => c.HistoricalPrices)
            .WithOne()
            .HasForeignKey(h => h.CryptoId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}