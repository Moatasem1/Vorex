using Microsoft.EntityFrameworkCore;
using Vorex.Domain.CryptoAnalyses;
using Vorex.Domain.Cryptos;
using Vorex.Domain.User;

namespace Vorex.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Crypto> Cryptos { get; set; }
    public DbSet<CryptoAnalysisHistory> CryptoAnalysisHistories { get; set; }
}