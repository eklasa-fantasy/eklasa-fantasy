using Results.API.Data;
using Results.API.Dtos;
using Results.API.Interfaces;
using Results.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Results.API.Services
{
    public class ResultsService : IResultsService
    {
        private readonly ResultsDbContext _context;
        private readonly IFootballApiService _apiService;

        public ResultsService(IFootballApiService footballApiService, ResultsDbContext context)
        {
            _apiService = footballApiService;
            _context = context;
        }

        public async Task<List<ResultDto>> GetResultsAll()
        {
            var results = await _context.Results
                .Include(r => r.GoalScorers)
                .Include(r => r.Cards)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.HomeSubs)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.AwaySubs)
                .ToListAsync();

            return results.Select(MapResultToResultDto).ToList();
        }

        public async Task<List<ResultDto>> GetResultsFromToDate(DateTime dateFrom, DateTime dateTo)
        {
            var results = await _context.Results
                .Where(r => r.Date >= dateFrom && r.Date <= dateTo)
                .Include(r => r.GoalScorers)
                .Include(r => r.Cards)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.HomeSubs)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.AwaySubs)
                .ToListAsync();

            return results.Select(MapResultToResultDto).ToList();
        }

        public async Task<List<ResultDto>> GetResultsByTeam(int teamId)
        {
            var results = await _context.Results
                .Where(r => r.HomeTeamId == teamId || r.AwayTeamId == teamId)
                .Include(r => r.GoalScorers)
                .Include(r => r.Cards)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.HomeSubs)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.AwaySubs)
                .ToListAsync();

            return results.Select(MapResultToResultDto).ToList();
        }

        public async Task<List<ResultDto>> GetResultsByRound(int round)
        {
            var results = await _context.Results
                .Where(r => r.Round == round)
                .Include(r => r.GoalScorers)
                .Include(r => r.Cards)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.HomeSubs)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.AwaySubs)
                .ToListAsync();

            return results.Select(MapResultToResultDto).ToList();
        }

        public async Task<List<ResultDto>> GetLiveScores()
        {
            var results = await _context.Results
                .Where(r => r.isMatchLive)
                .Include(r => r.GoalScorers)
                .Include(r => r.Cards)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.HomeSubs)
                .Include(r => r.Substitutions)
                    .ThenInclude(s => s.AwaySubs)
                .ToListAsync();

            return results.Select(MapResultToResultDto).ToList();
        }

        private ResultDto MapResultToResultDto(Result result)
        {
            return new ResultDto
            {
                MatchId = result.MatchId,
                Time = result.Time.ToString("HH:mm"),
                HomeTeamName = result.HomeTeamName,
                AwayTeamName = result.AwayTeamName,
                HomeTeamId = result.HomeTeamId,
                AwayTeamId = result.AwayTeamId,
                Date = result.Date.ToString("yyyy-MM-dd"),
                Round = result.Round,
                HomeTeamBadge = result.HomeTeamBadge,
                AwayTeamBadge = result.AwayTeamBadge,
                HomeTeamScore = result.HomeTeamScore,
                AwayTeamScore = result.AwayTeamScore,
                isMatchLive = result.isMatchLive,
                GoalScorers = result.GoalScorers?.Select(gs => new GoalscorerDto
                {
                    TimeScored = gs.TimeScored,
                    HomeScorer = gs.HomeScorer,
                    AwayScorer = gs.AwayScorer,
                    HomeAssist = gs.HomeAssist,
                    AwayAssist = gs.AwayAssist,
                    Score = gs.Score
                }).ToList(),
                Cards = result.Cards?.Select(c => new CardsDto
                {
                    TimeReceived = c.TimeReceived,
                    HomeFault = c.HomeFault,
                    AwayFault = c.AwayFault,
                    Card = c.Card
                }).ToList(),
                Substitutions = result.Substitutions != null ? new SubsDto
                {
                    HomeSubs = result.Substitutions.HomeSubs.Select(hs => new SubsHomeDto
                    {
                        Time = hs.Time,
                        Substitution = hs.Substitution
                    }).ToList(),
                    AwaySubs = result.Substitutions.AwaySubs.Select(hs => new SubsAwayDto
                    {
                        Time = hs.Time,
                        Substitution = hs.Substitution
                    }).ToList()
                } : null
            };
        }
    }
}
