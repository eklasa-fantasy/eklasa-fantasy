using Microsoft.AspNetCore.Mvc;
using Results.API.Dtos;
using Results.API.Interfaces;
using Results.API.Services;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Results.API.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IFootballApiService _footballApiService;
        private readonly IResultsService _resultsService;

        private readonly ITableService _tableService;


        public ResultsController(IFootballApiService footballApiService, IResultsService resultsService, ITableService tableService)
        {

            _footballApiService = footballApiService;
            _resultsService = resultsService;
            _tableService = tableService;

        }

        [HttpGet("footballapi")] // request powinien być wykonywany co określony czas, poniższa metoda zostaje w celu debugowania

        public async Task<IActionResult> GetResults()
        {
            try
            {
                string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                DateTime weekAgo = DateTime.Now.AddDays(-7);
                string dateWeekAgo = weekAgo.Year.ToString() + "-" + weekAgo.Month.ToString() + "-" + weekAgo.Day.ToString();
                var results = await _footballApiService.GetResultsAsync(dateWeekAgo, dateToday);

                if (results == null || !results.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(results);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var matches = await _resultsService.GetResultsAll();

                if (matches == null || !matches.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("fromToDate")]
        public async Task<IActionResult> GetResultsFromToDate([FromQuery] ResultsFromToDateDtoRequest resultsFromToDateDto)
        {
            try
            {
                var matches = await _resultsService.GetResultsFromToDate(resultsFromToDateDto.DateFrom, resultsFromToDateDto.DateTo);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("team")]
        public async Task<IActionResult> GetResultsByTeam([FromQuery] ResultsTeamDtoRequest resultsTeamDto)
        {
            try
            {
                var matches = await _resultsService.GetResultsByTeam(resultsTeamDto.teamId);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("round")]
        public async Task<IActionResult> GetResultsByRound([FromQuery] ResultsRoundDtoRequest roundDto)
        {
            try
            {
                var matches = await _resultsService.GetResultsByRound(roundDto.round);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }


        [HttpGet("livescore")]
        public async Task<IActionResult> GetLiveScore()
        {
            try
            {
                var matches = await _resultsService.GetLiveScores();

                if (matches == null || !matches.Any())
                {
                    return NotFound("No results found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("table")]
        public async Task<IActionResult> GetTable()
        {
            try
            {
                var table = await _tableService.CalculateTable();

                if (table == null || table.Teams.Count() != 18)
                {
                    return NotFound("Error occured while creating the table.");
                }

                return Ok(table);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


    }


}
