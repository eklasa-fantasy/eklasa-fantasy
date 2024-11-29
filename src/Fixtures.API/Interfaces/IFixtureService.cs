

using Fixtures.API.Dtos;
using Fixtures.API.Models;

namespace Fixtures.API.Interfaces
{
    public interface IFixtureService
    {
        Task<List<FixtureDto>> GetFixturesAll();
        Task<List<FixtureDto>> GetFixturesByTeam(int teamId);
        Task<List<FixtureDto>> GetFixturesFromToDate(DateTime dateFrom, DateTime dateTo);

        Task<List<FixtureDto>> GetFixturesByRound(int round);

        Task SaveApiFixturesToDatabase(List<APIFixtureDto> apiFixtures);
        Task SeedDatabase();
    }
}