using Microsoft.AspNetCore.Mvc;
using Results.API.Interfaces;
using RouteAttribute = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace Results.API.Controllers
{
    [Route("api/results")]
    [ApiController]
    public class ResultsController : ControllerBase
    {
        private readonly IFootballApiService _footballApiService;


        public ResultsController(IFootballApiService footballApiService)
        {

            _footballApiService = footballApiService;

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
                    return NotFound("No fixtures found.");
                }

                return Ok(results);

            }catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }

        }
    }


}
