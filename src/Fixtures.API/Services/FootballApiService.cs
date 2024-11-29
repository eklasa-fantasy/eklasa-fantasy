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
                //List<APIFixtureDto> list = new List<APIFixtureDto>();
                HttpResponseMessage response = await client.GetAsync($"https://apiv3.apifootball.com/?action=get_events&from={dateFrom}&to={dateTo}&league_id=259&APIkey={apiKey}");
                response.EnsureSuccessStatusCode(); // Rzuca wyjątek, jeśli kod odpowiedzi jest błędny
                string responseBody = await response.Content.ReadAsStringAsync();

                return await DeserializeFixturesAsync(responseBody);
                
            }
            catch (HttpRequestException ex) {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return null;
             }
             catch(Exception ex){
                Console.WriteLine($"Error: {ex.Message}");
                return null;
             }



        }

        public async Task<List<APIFixtureDto>> DeserializeFixturesAsync(string response){ //Prawdopodobnie nie potrzeba tu metody asynchronicznej
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try{
            var matches = JsonSerializer.Deserialize<List<APIFixtureDto>>(response, options);
            return matches;
            }
                catch(JsonException ex){
                    Console.WriteLine($"Json Error: {ex.Message}");
                    return null;
            }

        }

    }


}