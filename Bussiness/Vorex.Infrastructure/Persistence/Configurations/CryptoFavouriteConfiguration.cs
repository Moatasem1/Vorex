using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.User;

namespace Vorex.Infrastructure.Persistence.Configurations
{
    class CryptoFavouriteConfiguration : IEntityTypeConfiguration<CryptoFavorite>
    {
        public void Configure(EntityTypeBuilder<CryptoFavorite> builder)
        {
            builder.HasKey(cf => new { cf.UserId, cf.CryptoAnalysisHistoryId });

            builder.Property(cf => cf.UserId)
                .IsRequired();

            builder.Property(cf => cf.CryptoAnalysisHistoryId)
                .IsRequired();

            builder.Property(cf => cf.CreatedAt)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");
        }
    }
}