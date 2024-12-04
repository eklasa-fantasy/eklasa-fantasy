using Results.API.Dtos;

namespace Results.API.Interfaces
{
    public interface IFootballApiService
    {
        Task<List<APIResultDto>> GetResultsAsync(string dateFrom, string dateTo);


        Task<List<APITeamDto>> GetTeamIds();
        
        
        
    }
}