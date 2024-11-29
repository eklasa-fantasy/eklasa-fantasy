
using Fixtures.API.Dtos;
using Fixtures.API.Interfaces;
using Fixtures.API.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;


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

        [HttpGet("footballapi")] // request powinien być wykonywany co określony czas, poniższa metoda zostaje w celu debugowania

        public async Task<IActionResult> GetFixtures()
        {
            try
            {
                //TODO Metoda będzie używana do pobrania meczów z bazy danych za pomocą FixturesService
                string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                var matches = await _footballApiService.GetFixturesAsync(dateToday, "2025-06-01");

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found.");
                }

                return Ok(matches);
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
                var matches = await _fixtureService.GetFixturesAll();

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("fromToDate")]

        public async Task<IActionResult> GetFixturesFromToDate([FromQuery] FixturesFromToDateDtoRequest fixturesFromToDateDto)
        {
            try
            {
                var matches = await _fixtureService.GetFixturesFromToDate(fixturesFromToDateDto.DateFrom, fixturesFromToDateDto.DateTo);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found.");
                }

                return Ok(matches);

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }

        [HttpGet("team")]

        public async Task<IActionResult> GetFixturesByTeam([FromQuery] FixturesTeamDtoRequest fixturesTeamDto)
        {
            try
            {
                var matches = await _fixtureService.GetFixturesByTeam(fixturesTeamDto.teamId);

                if (matches == null || !matches.Any())
                {
                    return NotFound("No fixtures found.");
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