using Microsoft.EntityFrameworkCore;
using Results.API.Models;

namespace Results.API.Data
{
    public class ResultsDbContext : DbContext
    {
        public ResultsDbContext(DbContextOptions<ResultsDbContext> options) : base(options) { }

        public DbSet<Result> Results { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Result>()
                .ToTable("Results")
                .HasKey(r => r.Id);

            modelBuilder.Entity<Result>()
                .Property(r => r.Date)
                .HasColumnType("datetime2");

            modelBuilder.Entity<Result>()
                .HasMany(r => r.GoalScorers)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .HasMany(r => r.Cards)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Result>()
                .OwnsOne(r => r.Substitutions, navigationBuilder =>
                {
                    navigationBuilder.ToTable("Substitutions");
                    navigationBuilder.OwnsMany(s => s.HomeSubs, hs =>
                    {
                        hs.ToTable("HomeSubstitutions");
                        hs.Property(h => h.Time).IsRequired();
                        hs.Property(h => h.Substitution).IsRequired();
                    });

                    navigationBuilder.OwnsMany(s => s.AwaySubs, hs =>
                    {
                        hs.ToTable("AwaySubstitutions");
                        hs.Property(a => a.Time).IsRequired();
                        hs.Property(a => a.Substitution).IsRequired();
                    });
        });

            modelBuilder.Entity<Goalscorer>()
                .ToTable("Goalscorers")
                .HasKey(g => new { g.TimeScored, g.HomeScorer, g.AwayScorer
    });

            modelBuilder.Entity<Cards>()
                .ToTable("Cards")
                .HasKey(c => new { c.TimeReceived, c.HomeFault, c.AwayFault
});
        }
    }
}
