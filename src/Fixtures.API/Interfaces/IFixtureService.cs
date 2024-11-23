

using Fixtures.API.Dtos;
using Fixtures.API.Models;

namespace Fixtures.API.Interfaces
{
    public interface IFixtureService
    {
        Task<List<Fixture>> GetFixturesAll();
        Task<List<Fixture>> GetFixturesTeam(int teamId);
        Task<List<Fixture>> GetFixturesDate(DateTime dateFrom, DateTime dateTo);
        Task SaveApiFixturesToDatabase(List<APIFixtureDto> apiFixtures);
    }
}