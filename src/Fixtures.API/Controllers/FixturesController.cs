using Fixtures.API.Dtos;
using Fixtures.API.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Fixtures.API.Controllers
{
    [Route("api/fixtures")]
    [ApiController]
    public class FixturesController : ControllerBase
    {
        private readonly IFootballApiService _footballApiService;
        private readonly IFixtureService _fixtureService;

        public FixturesController(IFootballApiService footballApiService, IFixtureService fixtureService)
        {
            _footballApiService = footballApiService;
            _fixtureService = fixtureService;
        }

        // Debugowanie: Pobieranie danych z API bez zapisywania
        [HttpGet("fetch-from-api")]
        public async Task<IActionResult> FetchFromApi(string dateFrom, string dateTo)
        {
            try
            {
                var matches = await _footballApiService.GetFixturesAsync(dateFrom, dateTo);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found in the API.");
                }

                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Pobieranie danych z API i zapisywanie ich w bazie danych
        [HttpPost("fetch-and-save")]
        public async Task<IActionResult> FetchAndSave(string dateFrom, string dateTo)
        {
            try
            {
                // Pobieranie danych z API
                var apiFixtures = await _footballApiService.GetFixturesAsync(dateFrom, dateTo);

                if (apiFixtures == null || !apiFixtures.Any())
                {
                    return NotFound("No fixtures found in the API.");
                }

                // Zapisywanie danych do bazy
                await _fixtureService.SaveApiFixturesToDatabase(apiFixtures);

                return Ok("Fixtures fetched from API and saved to the database.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Pobieranie wszystkich meczów z bazy danych
        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var matches = await _fixtureService.GetFixturesAll();

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found in the database.");
                }

                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Pobieranie meczów na podstawie zakresu dat
        [HttpPost("from-to-date")]
        public async Task<IActionResult> GetFromToDate([FromBody] FixturesFromToDateDto fixturesFromToDateDto)
        {
            try
            {
                var matches = await _fixtureService.GetFixturesDate(fixturesFromToDateDto.DateFrom, fixturesFromToDateDto.DateTo);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found in the given date range.");
                }

                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        // Pobieranie meczów związanych z konkretną drużyną
        [HttpPost("team")]
        public async Task<IActionResult> GetTeam([FromBody] FixturesTeamDto fixturesTeamDto)
        {
            try
            {
                var matches = await _fixtureService.GetFixturesTeam(fixturesTeamDto.teamId);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found for the given team.");
                }

                return Ok(matches);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }
    }
}
