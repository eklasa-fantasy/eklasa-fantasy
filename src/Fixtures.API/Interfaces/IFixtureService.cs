

using Fixtures.API.Models;

namespace Fixtures.API.Interfaces
{
    public interface IFixtureService
    {
        Task<List<Fixture>> GetFixturesAll();
        Task<List<Fixture>> GetFixturesTeam(int teamId);
        Task<List<Fixture>> GetFixturesDate(string dateFrom, string dateTo);
    }
}