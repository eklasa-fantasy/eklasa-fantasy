using System.Collections.Generic;
using Fixtures.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fixtures.API.Data
{
    public class FixturesDbContext : DbContext
    {
        public FixturesDbContext(DbContextOptions<FixturesDbContext> options) : base(options)
        {
        }

        public DbSet<Fixture> Fixtures { get; set; }

    }
}
