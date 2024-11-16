
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

    public class FixturesController : ControllerBase{
        private readonly IFootballApiService _footballApiService;

        public FixturesController(IFootballApiService footballApiService)
        {
            _footballApiService = footballApiService;
        }

        [HttpGet("all")]

        public async Task<IActionResult> GetFixtures(){
            try{
                //TODO Metoda będzie używana do pobrania meczów z bazy danych za pomocą FixturesService
                string dateToday = DateTime.Now.Year.ToString() + "-" + DateTime.Now.Month.ToString() + "-" + DateTime.Now.Day.ToString();
                var matches = await _footballApiService.GetFixturesAsync(dateToday,"2025-06-01");

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