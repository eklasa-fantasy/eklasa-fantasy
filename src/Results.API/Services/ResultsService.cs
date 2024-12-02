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
            await SeedDatabase();

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

        public async Task SeedDatabase()
        {
            if (!_context.Results.Any())
            {

                var resultDtos = await _apiService.GetResultsAsync("2024-12-30", $"{DateTime.Now:yyyy-MM-dd}");

                await this.SaveApiFixturesToDatabase(resultDtos);
            }
        }

        public async Task SaveApiFixturesToDatabase(List<APIResultDto> apiResults)
        {
            foreach (var apiResult in apiResults)
            {
                try
                {
                    var result = new Result
                    {
                        MatchId = int.TryParse(apiResult.Id, out var id) ? id : throw new FormatException("Invalid Id"),
                        Time = DateTime.TryParse(apiResult.Time, out var time) ? time : throw new FormatException("Invalid Time"),
                        HomeTeamName = apiResult.HomeTeamName ?? throw new ArgumentNullException(nameof(apiResult.HomeTeamName)),
                        AwayTeamName = apiResult.AwayTeamName ?? throw new ArgumentNullException(nameof(apiResult.AwayTeamName)),
                        HomeTeamId = int.TryParse(apiResult.HomeTeamId, out var homeTeamId) ? homeTeamId : throw new FormatException("Invalid HomeTeamId"),
                        AwayTeamId = int.TryParse(apiResult.AwayTeamId, out var awayTeamId) ? awayTeamId : throw new FormatException("Invalid AwayTeamId"),
                        Date = DateTime.TryParse(apiResult.Date, out var date) ? date : throw new FormatException("Invalid Date"),
                        Round = int.TryParse(apiResult.Round, out var round) ? round : throw new FormatException("Invalid Round"),
                        HomeTeamBadge = apiResult.HomeTeamBadge,
                        AwayTeamBadge = apiResult.AwayTeamBadge,
                        HomeTeamScore = int.TryParse(apiResult.HomeTeamScore, out var homeTeamScore) ? homeTeamScore : throw new FormatException("Invalid HomeTeamScore"),
                        AwayTeamScore = int.TryParse(apiResult.AwayTeamScore, out var awayTeamScore) ? awayTeamScore : throw new FormatException("Invalid AwayTeamScore"),
                        isMatchLive = bool.TryParse(apiResult.isMatchLive, out var isMatchLive) ? isMatchLive : throw new FormatException("Invalid isMatchLive"),
                        GoalScorers = MapGoalscorerDtoToModel(apiResult),

                        Cards = MapCardsDtoToModel(apiResult),

                        Substitutions = new Subs
                        {
                            HomeSubs = MapHomeSubsDtoToModel(apiResult),
                            AwaySubs = MapAwaySubsDtoToModel(apiResult)
                        }
                    };


                    // Dodanie nowego rekordu do kontekstu bazy danych
                    await _context.Results.AddAsync(result);
                    foreach (var goalscorer in result.GoalScorers)
                    {
                        await _context.Goalscorers.AddAsync(goalscorer);
                    }

                    foreach (var card in result.Cards)
                    {
                        await _context.Cards.AddAsync(card);
                    }

                    foreach (var homeSub in result.Substitutions.HomeSubs)
                    {
                        await _context.SubsHome.AddAsync(homeSub);
                    }

                    foreach (var awaySub in result.Substitutions.AwaySubs)
                    {
                        await _context.SubsAway.AddAsync(awaySub);
                    }

                    await _context.Subs.AddAsync(result.Substitutions);

                    await _context.SaveChangesAsync();
                }
                catch (Exception ex)
                {
                    // Logowanie b³êdów lub inne dzia³ania
                    Console.WriteLine($"Error processing fixture: {ex.Message}");
                }
            }

            // Zapisanie zmian w bazie danych
            await _context.SaveChangesAsync();
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
                    Card = c.CardType
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

        private List<SubHome> MapHomeSubsDtoToModel(APIResultDto apiResultDto)
        {
            var result = apiResultDto.Substitutions.HomeSubs.Select(result => new SubHome
            {
                Time = result.Time,
                Substitution = result.Substitution,
            }).ToList();

            return result;
        }

        private List<SubAway> MapAwaySubsDtoToModel(APIResultDto apiResultDto)
        {
            var result = apiResultDto.Substitutions.AwaySubs.Select(result => new SubAway
            {
                Time = result.Time,
                Substitution = result.Substitution,
            }).ToList();

            return result;
        }

        private List<Card> MapCardsDtoToModel(APIResultDto apiResultDto)
        {
            var result = apiResultDto.Cards.Select(result => new Card
            {
                TimeReceived = result.TimeReceived,
                HomeFault = result.HomeFault,
                AwayFault = result.AwayFault,
                CardType = result.Card,
            }).ToList();

            return result;
        }

        private List<Goalscorer> MapGoalscorerDtoToModel(APIResultDto apiResultDto)
        {
            var result = apiResultDto.GoalScorers.Select(result => new Goalscorer
            {
                Score = result.Score,
                TimeScored = result.TimeScored,
                HomeScorer = result.HomeScorer,
                AwayScorer = result.AwayScorer,
                HomeAssist = result.HomeAssist,
                AwayAssist = result.AwayAssist,

            }).ToList();

            return result;
        }
    }
}
