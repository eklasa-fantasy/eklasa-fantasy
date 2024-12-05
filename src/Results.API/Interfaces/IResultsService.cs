

using Results.API.Dtos;

namespace Results.API.Interfaces
{
    public interface IResultsService
    {
        Task<List<ResultDto>> GetResultsAll();
        Task<List<ResultDto>> GetResultsByTeam(int teamId);
        Task<List<ResultDto>> GetResultsFromToDate(DateTime dateFrom, DateTime dateTo);

        Task<List<ResultDto>> GetResultsByRound(int round);
        Task<List<ResultDto>> GetLiveScores();
        Task SeedDatabase();
    }
}