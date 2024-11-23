using Fixtures.API.Interfaces;
using Fixtures.API.Dtos;
using System.Net.Http;
using System.Text.Json;

namespace Fixtures.API.Services
{
    public class FootballApiService : IFootballApiService
    {
        private static readonly HttpClient client = new HttpClient();
        private readonly string _apiKey;

        public FootballApiService()
        {
            // Pobranie klucza API z ustawień środowiska
            _apiKey = Environment.GetEnvironmentVariable("API_KEY") ?? throw new Exception("API key not found.");
        }

        public async Task<List<APIFixtureDto>> GetFixturesAsync(string dateFrom, string dateTo)
        {
            try
            {
                // Budowanie adresu żądania
                string url = $"https://apiv3.apifootball.com/?action=get_events&from={dateFrom}&to={dateTo}&league_id=153&APIkey={_apiKey}";

                // Wysłanie żądania HTTP
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode(); // Rzuca wyjątek, jeśli odpowiedź HTTP wskazuje na błąd

                // Odczytanie treści odpowiedzi
                string responseBody = await response.Content.ReadAsStringAsync();

                // Deserializacja odpowiedzi do listy APIFixtureDto
                return await DeserializeFixturesAsync(responseBody);
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

        public async Task<List<APIFixtureDto>> DeserializeFixturesAsync(string response)
        {
            var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

            try
            {
                // Deserializacja JSON do listy APIFixtureDto
                var matches = JsonSerializer.Deserialize<List<APIFixtureDto>>(response, options);
                return matches;
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"Json Error: {ex.Message}");
                return null;
            }
        }
    }
}
