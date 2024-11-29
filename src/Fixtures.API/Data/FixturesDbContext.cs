using Fixtures.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fixtures.API.Data
{
    public class FixturesDbContext : DbContext
    {
        public FixturesDbContext(DbContextOptions<FixturesDbContext> options)
            : base(options)
        {
        }

        public DbSet<Fixture> Fixtures { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Fixture>(entity =>
            {
                entity.HasKey(f => f.Id);
                entity.Property(f => f.HomeTeamName).IsRequired();
                entity.Property(f => f.AwayTeamName).IsRequired();
                // Dodaj inne wymagania i relacje, jeśli są potrzebne
            });
        }
    }
}
