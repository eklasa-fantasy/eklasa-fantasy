using System.Collections.Generic;
using System.Reflection.Emit;
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
            base.OnModelCreating(modelBuilder);

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
                .OwnsOne(r => r.Substitutions);
        }
    }
}
