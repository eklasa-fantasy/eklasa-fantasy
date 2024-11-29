

using Fixtures.API.Dtos;
using Fixtures.API.Models;

namespace Fixtures.API.Interfaces
{
    public interface IFixtureService
    {
        Task<List<FixtureDto>> GetFixturesAll();
        Task<List<FixtureDto>> GetFixturesTeam(int teamId);
        Task<List<FixtureDto>> GetFixturesDate(DateTime dateFrom, DateTime dateTo);
        Task SaveApiFixturesToDatabase(List<APIFixtureDto> apiFixtures);
        Task SeedDatabase();
    }
}