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
        protected readonly ResultsDbContext _context;

        private readonly IFootballApiService _apiService;

        public TableService(IResultsService resultsService, ResultsDbContext context, IFootballApiService footballApiService)
        {
            _resultsService = resultsService;
            _context = context;
            _apiService = footballApiService;
        }

        public async Task<TableDto> GetTableDtoAsync(){
            if(await _context.Tables.AnyAsync()){
                //TODO: update istniejacej tabeli na podstawie wynikow, ew. uzycie schedulera
                return await MapTableToTableDtoAsync();
            }
            else
            {
                if(!await _context.Results.AnyAsync()){
                    await _resultsService.SeedDatabase();
                }
  
                var table = await CalculateTable();
                return table;
            }
        }

        public async Task<TableDto> CalculateTable()
        {       
            var results = await _context.Results
                .ToListAsync();

            var teamEntries = await InitTableModels();

            foreach (var result in results)
            {
                await CalculateTeamModelsStatsAsync(teamEntries, result);
            }

            foreach (var team in teamEntries){
                await _context.TableTeams.AddAsync(team);
            }
            await _context.SaveChangesAsync();

            var updatedTeams = await _context.TableTeams.ToListAsync();

            var table = new Table
            {
                Teams = updatedTeams
            };

            table.Teams.Sort((t1, t2) =>
            {
                int pointComparison = t2.Points.CompareTo(t1.Points);

                if(pointComparison == 0){
                    return t2.GoalsDiff.CompareTo(t1.GoalsDiff);
                }
                return pointComparison;
            });

            await _context.Tables.AddAsync(table);
            await _context.SaveChangesAsync();

            var tableDto = await MapTableToTableDtoAsync();

            return tableDto;
        }

        private async Task<TableDto> MapTableToTableDtoAsync(){
            //var table = await _context.Tables.FirstAsync();

            var tableTeams = await _context.TableTeams.ToListAsync();

            tableTeams.Sort((t1, t2) =>
            {
                int pointComparison = t2.Points.CompareTo(t1.Points);

                if(pointComparison == 0){
                    return t2.GoalsDiff.CompareTo(t1.GoalsDiff);
                }
                return pointComparison;
            });

            var tableDto = new TableDto 
            {
                Teams = tableTeams.Select(team => new TableTeamDto
                {
                    TeamId = team.TeamId,
                    TeamBadge = team.TeamBadge,
                    TeamName = team.TeamName,
                    Played = team.Played,
                    Wins = team.Wins,
                    Loses = team.Loses,
                    Draws = team.Draws,
                    Points = team.Points,
                    GoalsF = team.GoalsF,
                    GoalsA = team.GoalsA,
                    GoalsDiff = team.GoalsDiff
                }).ToList()
            };

            return tableDto;
        }


        public async Task CalculateTeamModelsStatsAsync(List<TableTeam> teamEntries, Result result)
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

        public async Task<List<TableTeam>> InitTableModels()
        {
            var teams = await _apiService.GetTeamIds();

            var teamEntries = new List<TableTeam>();

            foreach (var team in teams)
            {
                var teamEntry = new TableTeam
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