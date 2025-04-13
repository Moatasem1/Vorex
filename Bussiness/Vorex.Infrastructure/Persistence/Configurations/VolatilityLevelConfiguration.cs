using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.Common;
using Vorex.Domain.Cryptos;

namespace Vorex.Infrastructure.Persistence.Configurations;

class VolatilityLevelConfiguration : IEntityTypeConfiguration<VolatilityLevel>
{
    public void Configure(EntityTypeBuilder<VolatilityLevel> builder)
    {
        builder.HasKey(v => v.Id);

        builder.Property(v => v.Id)
               .ValueGeneratedNever(); 

        builder.Property(v => v.Name)
               .IsRequired()
               .HasMaxLength(15);

        builder.HasData(
            [.. Enumeration.GetAll<VolatilityLevel>()]
            );
    }
}