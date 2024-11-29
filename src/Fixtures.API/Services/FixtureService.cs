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
        private readonly IFootballApiService _apiService;

        public FixtureService(FixturesDbContext context, IFootballApiService footballApiService)
        {
            _context = context;
            _apiService = footballApiService;
        }

        public async Task<List<FixtureDto>> GetFixturesAll()
        {
            await SeedDatabase();
            // Pobranie wszystkich fixture'ów z bazy danych
            var fixtures = await _context.Fixtures.ToListAsync();

            return await MapFixtureToFixtureDto(fixtures);
        }

        public async Task<List<FixtureDto>> GetFixturesFromToDate(DateTime dateFrom, DateTime dateTo)
        {
            await SeedDatabase();
            // Pobranie fixture'ów na podstawie zakresu dat

            var fixtures = await _context.Fixtures
                .Where(f => f.Date >= dateFrom && f.Date <= dateTo)
                .ToListAsync();

            return await MapFixtureToFixtureDto(fixtures);
        }

        public async Task<List<FixtureDto>> GetFixturesByTeam(int teamId)
        {
            await SeedDatabase();
            // Pobranie fixture'ów związanych z określoną drużyną
            var fixtures = await _context.Fixtures
                .Where(f => f.HomeTeamId == teamId || f.AwayTeamId == teamId)
                .ToListAsync();

            return await MapFixtureToFixtureDto(fixtures);
        }

        public async Task SaveApiFixturesToDatabase(List<APIFixtureDto> apiFixtures)
        {
            foreach (var apiFixture in apiFixtures)
            {
                try
                {
                    var fixture = new Fixture
                    {
                        MatchId = int.TryParse(apiFixture.Id, out var id) ? id : throw new FormatException("Invalid Id"),
                        Time = DateTime.TryParse(apiFixture.Time, out var time) ? time : throw new FormatException("Invalid Time"),
                        HomeTeamName = apiFixture.HomeTeamName ?? throw new ArgumentNullException(nameof(apiFixture.HomeTeamName)),
                        AwayTeamName = apiFixture.AwayTeamName ?? throw new ArgumentNullException(nameof(apiFixture.AwayTeamName)),
                        HomeTeamId = int.TryParse(apiFixture.HomeTeamId, out var homeTeamId) ? homeTeamId : throw new FormatException("Invalid HomeTeamId"),
                        AwayTeamId = int.TryParse(apiFixture.AwayTeamId, out var awayTeamId) ? awayTeamId : throw new FormatException("Invalid AwayTeamId"),
                        Date = DateTime.TryParse(apiFixture.Date, out var date) ? date : throw new FormatException("Invalid Date"),
                        Round = int.TryParse(apiFixture.Round, out var round) ? round : throw new FormatException("Invalid Round"),
                        HomeTeamBadge = apiFixture.HomeTeamBadge,
                        AwayTeamBadge = apiFixture.AwayTeamBadge
                    };

                    // Dodanie nowego rekordu do kontekstu bazy danych
                    await _context.Fixtures.AddAsync(fixture);
                    await _context.SaveChangesAsync();
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

        public async Task SeedDatabase()
        {
            if(!_context.Fixtures.Any()){
                
                var matches = await _apiService.GetFixturesAsync($"{DateTime.Now:yyyy-MM-dd}", "2025-06-01");

                await  this.SaveApiFixturesToDatabase(matches);
            }
        }

        private async Task<List<FixtureDto>> MapFixtureToFixtureDto(List<Fixture> fixtures){

            List<FixtureDto> fixtureDtos = new List<FixtureDto>();

            foreach(var fixture in fixtures){
                var fixtureDto = new FixtureDto{
                    MatchId = fixture.Id,
                    Time = fixture.Time.ToShortTimeString(),
                    Date = fixture.Date.ToShortDateString(),
                    HomeTeamName = fixture.HomeTeamName,
                    AwayTeamName = fixture.AwayTeamName,
                    HomeTeamId = fixture.HomeTeamId,
                    AwayTeamId = fixture.AwayTeamId,
                    HomeTeamBadge = fixture.HomeTeamBadge,
                    AwayTeamBadge = fixture.AwayTeamBadge,
                    Round = fixture.Round,
                };
                fixtureDtos.Add(fixtureDto);
            }

            return fixtureDtos;
        }
    }
}
