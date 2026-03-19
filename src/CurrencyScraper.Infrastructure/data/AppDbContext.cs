using CurrencyScraper.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace CurrencyScraper.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Currency> Currencies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Currency>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Code).IsRequired().HasMaxLength(10);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Bid).HasPrecision(18, 4);
                entity.Property(e => e.Ask).HasPrecision(18, 4);
            });
        }
    }
}
