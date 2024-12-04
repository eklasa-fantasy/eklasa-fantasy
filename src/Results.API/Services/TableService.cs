using Results.API.Data;
using Results.API.Dtos;
using Results.API.Interfaces;
using Results.API.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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

            //TODO sprawdzenie czy w bazie danych istnieja juz rekordy odnoszace sie do druzyn w tabeli, jesli nie to inicjuje tabele ligowa
            var teamEntries = await InitTable();

            foreach (var result in results)
            {
                CalculateTeamStatsAsync(teamEntries, result);
            }

            var table = new TableDto
            {
                Teams = teamEntries
            };

            table.Teams.Sort((t1, t2) =>
            {
                int pointComparison = t1.Points.CompareTo(t2.Points);

                if(pointComparison == 0){
                    return t1.GoalsDiff.CompareTo(t2.GoalsDiff);
                }
                return pointComparison;
            });

            return table;

        }


        public void CalculateTeamStatsAsync(List<TableTeamDto> teamEntries, Result result)
        {

            var teamHome = teamEntries.FirstOrDefault(t => t.TeamId == result.HomeTeamId);
            var teamAway = teamEntries.FirstOrDefault(t => t.TeamId == result.AwayTeamId);
            teamHome.Played += 1;
            teamAway.Played += 1;

            teamHome.GoalsF += result.HomeTeamScore;
            teamHome.GoalsA += result.AwayTeamScore;
            teamHome.GoalsDiff = teamHome.GoalsF - teamHome.GoalsA;

            teamAway.GoalsF += result.AwayTeamScore;
            teamAway.GoalsA += result.HomeTeamScore;
            teamAway.GoalsDiff = teamAway.GoalsF - teamAway.GoalsA;

            if (result.HomeTeamScore > result.AwayTeamScore)
            {
                teamHome.Points += 3;
                teamHome.Wins += 1;

                teamAway.Loses += 1;
            }
            else if (result.HomeTeamScore < result.AwayTeamScore)
            {
                teamAway.Points += 3;
                teamAway.Wins += 1;

                teamHome.Loses += 1;
            }
            else
            {
                teamHome.Points += 1;
                teamAway.Points += 1;

                teamHome.Draws += 1;
                teamAway.Draws += 1;
            }


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