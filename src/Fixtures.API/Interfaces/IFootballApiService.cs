using Fixtures.API.Dtos;
using Fixtures.API.Models;
using System.Threading.Tasks;

namespace Fixtures.API.Interfaces
{
    public interface IFootballApiService
    {
        Task<List<APIFixtureDto>> GetFixturesAsync(string dateFrom, string dateTo);
        //Task<List<APIFixtureDto>> DeserializeFixturesAsync(string response);
        
    }
}