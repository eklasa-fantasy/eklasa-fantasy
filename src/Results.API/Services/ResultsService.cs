

using Results.API.Dtos;
using Results.API.Interfaces;

namespace Results.API.Services
{
    public class ResultsService : IResultsService
    {
        private IFootballApiService _apiService;

        public ResultsService(IFootballApiService footballApiService)
        {
            _apiService = footballApiService;
        }

        public async Task<List<ResultDto>> GetResultsAll()
        {
           //TODO await SeedDatabase();
            
            //var results = await _context.Results.ToListAsync();

            //return await MapResultToResultDto(results);

            return null;
        }

         public async Task<List<ResultDto>> GetResultsFromToDate(DateTime dateFrom, DateTime dateTo)
        {
            //TODO await SeedDatabase();
            // Pobranie fixture'ów na podstawie zakresu dat

            //var results = await _context.Results
            //    .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
             //   .ToListAsync();

           //return await MapResultToResultDto(results);
           return null;
        }

        public async Task<List<ResultDto>> GetResultsByTeam(int teamId)
        {
            //await SeedDatabase();
            // Pobranie fixture'ów związanych z określoną drużyną
          //  var results = await _context.Results
               // .Where(r => r.HomeTeamId == teamId || r.AwayTeamId == teamId)
               // .ToListAsync();

           //return await MapResultToResultDto(results);

           return null;
        }

        public async Task<List<ResultDto>> GetResultsByRound(int round){
           // await SeedDatabase();

           // var results = await _context.Results
              //  .Where(r => r.Round == round)
              //  .ToListAsync();

           //return await MapResultToResultDto(results);
           return null;

        }

        public async Task<List<ResultDto>> GetLiveScores(){
            // await SeedDatabase();

            // var results = await _context.Results
               // .Where(r => r.isMatchLive == true)
              //  .ToListAsync();
            return null;
        }

    }
}