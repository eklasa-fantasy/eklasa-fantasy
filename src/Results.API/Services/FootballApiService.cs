using System.Text.Json;
using Results.API.Dtos;
using Results.API.Interfaces;

namespace Results.API.Services
{
    public class FootballApiService : IFootballApiService
    {
        private static readonly HttpClient client = new HttpClient();
        string apiKey = Environment.GetEnvironmentVariable("API_KEY");


        public async Task<List<APIResultDto>> GetResultsAsync(string dateFrom, string dateTo)
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync($"https://apiv3.apifootball.com/?action=get_events&from={dateFrom}&to={dateTo}&league_id=259&APIkey={apiKey}");
                response.EnsureSuccessStatusCode(); // Rzuca wyjątek, jeśli kod odpowiedzi jest błędny
                string responseBody = await response.Content.ReadAsStringAsync();

                return await DeserializeResultsAsync(responseBody);
            }
            catch (HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<List<APIResultDto>> DeserializeResultsAsync(string response)
        {

            //Console.WriteLine(response);

            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var results = JsonSerializer.Deserialize<List<APIResultDto>>(response, options);
                return results;

            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Json Error: {ex.Message}");
                return null;
            }

        }

        public async Task<List<APITeamDto>> GetTeamIds()
        {
            try
            {
            HttpResponseMessage response = await client.GetAsync($"https://apiv3.apifootball.com/?action=get_teams&league_id=259&APIkey={apiKey}");
            response.EnsureSuccessStatusCode(); // Rzuca wyjątek, jeśli kod odpowiedzi jest błędny
            string responseBody = await response.Content.ReadAsStringAsync();

            return await DeserializeTeamsAsync(responseBody);



            }catch(HttpRequestException ex)
            {
                Console.WriteLine($"HTTP error: {ex.Message}");
                return null;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }




        }

        public async Task<List<APITeamDto>> DeserializeTeamsAsync(string response)
        {
            try
            {
                var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

                var teams = JsonSerializer.Deserialize<List<APITeamDto>>(response, options);
                return teams;

            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Json Error: {ex.Message}");
                return null;
            }
        }
    }
}