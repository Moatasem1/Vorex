using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Vorex.Domain.Cryptos;
using Vorex.Domain.User;

namespace Vorex.Infrastructure.Persistence.Configurations;

public class RefreshTokenConfigruation : IEntityTypeConfiguration<RefreshToken>
{
    public void Configure(EntityTypeBuilder<RefreshToken> builder)
    {
       builder.HasKey(c => c.Id);

        builder.Property(cc => cc.CreatedAt)
           .IsRequired()
           .HasDefaultValueSql("GETDATE()");

        builder.Property(c => c.Token)
            .IsRequired();

        builder.Property(c => c.UserId)
            .IsRequired();

        builder.Property(c => c.ExpiresOn)
            .IsRequired();

        builder.HasOne<User>().WithMany()
            .HasForeignKey(c => c.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
