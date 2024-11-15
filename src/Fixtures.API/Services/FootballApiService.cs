using Microsoft.Extensions.Configuration;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Fixtures.API.Interfaces;
using Fixtures.API.Dtos;

namespace Fixtures.API.Services
{
    public class FootballApiService : IFootballApiService
    {
        private static readonly HttpClient client = new HttpClient();
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");
        
        public async Task<List<APIFixtureDto>> GetFixturesAsync(string dateFrom, string dateTo)
        {
            
            try
            {
                List<APIFixtureDto> list = new List<APIFixtureDto>();
                HttpResponseMessage response = await client.GetAsync($"https://apiv3.apifootball.com/?action=get_events&from={dateFrom}&to={dateTo}&league_id=153&APIkey=" + apiKey);
                response.EnsureSuccessStatusCode(); // Rzuca wyjątek, jeśli kod odpowiedzi jest błędny
                string responseBody = await response.Content.ReadAsStringAsync();
                return list;
            }
            catch (Exception ex) {
                return null;
             }



        }
    }


}