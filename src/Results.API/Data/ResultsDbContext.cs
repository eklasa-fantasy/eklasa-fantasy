using Microsoft.EntityFrameworkCore;
using Results.API.Models;

namespace Results.API.Data
{
    public class ResultsDbContext : DbContext
    {
        public ResultsDbContext(DbContextOptions<ResultsDbContext> options) : base(options) { }

        public DbSet<Result> Results { get; set; }
        public DbSet<Card> Cards { get; set; }
        public DbSet<Goalscorer> Goalscorers { get; set; }
        public DbSet<SubAway> SubsAway { get; set; }
        public DbSet<SubHome> SubsHome { get; set; }
        public DbSet<Subs> Subs { get; set; }
        public DbSet<Table> Tables {get; set;}
        public DbSet<TableTeam> TableTeams {get; set;}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

        }
    }
}
