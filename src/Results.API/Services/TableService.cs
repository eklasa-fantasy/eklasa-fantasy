using Results.API.Data;
using Results.API.Dtos;
using Results.API.Interfaces;
using Results.API.Models;
using Microsoft.EntityFrameworkCore;

namespace Results.API.Services
{
    public class TableService : ITableService
    {
        private readonly IResultsService _resultsService;
        private readonly ResultsDbContext _context;

        private readonly IFootballApiService _apiService;



        public TableService(IResultsService resultsService, ResultsDbContext context, IFootballApiService footballApiService)
        {
            _resultsService = resultsService;
            _context = context;
            _apiService = footballApiService;
        }


        public async Task<TableDto> CalculateTable()
        {

            var results = await _context.Results
                .ToListAsync();


            return null;

        }


        public async Task<List<TableTeamDto>> CalculateTeamStatsAsync()
        {
            //TODO sprawdzenie czy w bazie danych istnieja juz rekordy odnoszace sie do druzyn w tabeli, jesli nie to inicjuje tabele ligowa
            var teamEntries = InitTable();


            return null;
        }

        public async Task<List<TableTeamDto>> InitTable()
        {
            var teams = await _apiService.GetTeamIds();

            var teamEntries = new List<TableTeamDto>();

            foreach (var team in teams)
            {
                var teamEntry = new TableTeamDto
                {
                    TeamId = int.TryParse(team.TeamId, out var teamId) ? teamId : throw new FormatException("Invalid Id"),
                    TeamBadge = team.TeamBadge,
                    TeamName = team.TeamName,
                    Played = 0,
                    Wins = 0,
                    Loses = 0,
                    Draws = 0,
                    Points = 0,
                    GoalsF = 0,
                    GoalsA = 0,
                    GoalsDiff = 0
                };
                teamEntries.Add(teamEntry);
            }
            return teamEntries;
        }

    }


}