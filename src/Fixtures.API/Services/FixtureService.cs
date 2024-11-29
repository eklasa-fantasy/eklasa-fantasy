using Fixtures.API.Data;
using Fixtures.API.Dtos;
using Fixtures.API.Interfaces;
using Fixtures.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Fixtures.API.Services
{
    public class FixtureService : IFixtureService
    {
        private readonly FixturesDbContext _context;

        public FixtureService(FixturesDbContext context)
        {
            _context = context;
        }

        public async Task<List<Fixture>> GetFixturesAll()
        {
            // Pobranie wszystkich fixture'ów z bazy danych
            return await _context.Fixtures.ToListAsync();
        }

        public async Task<List<Fixture>> GetFixturesDate(DateTime dateFrom, DateTime dateTo)
        {
            // Pobranie fixture'ów na podstawie zakresu dat
            return await _context.Fixtures
                .Where(f => f.Date >= dateFrom && f.Date <= dateTo)
                .ToListAsync();
        }

        public async Task<List<Fixture>> GetFixturesTeam(int teamId)
        {
            // Pobranie fixture'ów związanych z określoną drużyną
            return await _context.Fixtures
                .Where(f => f.HomeTeamId == teamId || f.AwayTeamId == teamId)
                .ToListAsync();
        }

        public async Task SaveApiFixturesToDatabase(List<APIFixtureDto> apiFixtures)
        {
            foreach (var apiFixture in apiFixtures)
            {
                try
                {
                    var fixture = new Fixture
                    {
                        Id = int.Parse(apiFixture.Id),
                        Time = DateTime.Parse(apiFixture.Time),
                        HomeTeamName = apiFixture.HomeTeamName,
                        AwayTeamName = apiFixture.AwayTeamName,
                        HomeTeamId = int.Parse(apiFixture.HomeTeamId),
                        AwayTeamId = int.Parse(apiFixture.AwayTeamId),
                        Date = DateTime.Parse(apiFixture.Date),
                        Round = int.Parse(apiFixture.Round),
                        HomeTeamBadge = apiFixture.HomeTeamBadge,
                        AwayTeamBadge = apiFixture.AwayTeamBadge
                    };

                    // Dodanie nowego rekordu do kontekstu bazy danych
                    await _context.Fixtures.AddAsync(fixture);
                }
                catch (Exception ex)
                {
                    // Logowanie błędów lub inne działania
                    Console.WriteLine($"Error processing fixture: {ex.Message}");
                }
            }

            // Zapisanie zmian w bazie danych
            await _context.SaveChangesAsync();
        }
    }
}
