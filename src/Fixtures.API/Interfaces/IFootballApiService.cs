using Fixtures.API.Models;
using System.Threading.Tasks;

namespace Fixtures.API.Interfaces
{
    public interface IFootballApiService
    {
        Task<FixtureResponse> GetFixturesAsync(string dateFrom, string dateTo);
    }
}