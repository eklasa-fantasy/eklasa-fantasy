


using Fixtures.API.Interfaces;
using Fixtures.API.Models;

namespace Fixtures.API.Services
{
    public class FixtureService : IFixtureService
    {
        public Task<List<Fixture>> GetFixturesAll()
        {
             // w sumie nie potrzeba nam GetFixturesAll i GetFixturesDate, przecież wystarczy podać inne daty w FixturesController, ale zostawiam
            //DateTime dateToday = DateTime.Now;
            //DateTime dateEnd = DateTime.ParseExact("2025-06-01", "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            var fixtures = new List<Fixture>(); //TODO pobranie już istniejących modelów lub pobranie danych z bazy danych i konwersja ich na modele (Zależy jak Dominik będzie robił)


            return Task.FromResult(fixtures);
        }

        public Task<List<Fixture>> GetFixturesDate(DateTime dateFrom, DateTime dateTo)
        {
            var fixtures = new List<Fixture>(); //TODO pobranie już istniejących modelów lub pobranie danych z bazy danych i konwersja ich na modele (Zależy jak Dominik będzie robił)
            var filteredFixtures = fixtures.Where(f => f.Date >= dateFrom && f.Date <= dateTo).ToList();

            return Task.FromResult(filteredFixtures);
        }

        public Task<List<Fixture>> GetFixturesTeam(int teamId)
        {
            var fixtures = new List<Fixture>(); //TODO pobranie już istniejących...
            var filteredFixtures = fixtures.Where(f => f.HomeTeamId == teamId || f.AwayTeamId == teamId).ToList();

            return Task.FromResult(filteredFixtures);
        }
    }
}