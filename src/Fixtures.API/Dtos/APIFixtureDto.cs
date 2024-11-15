namespace Fixtures.API.Dtos
{
    
    public class APIFixtureDto
    {
        [JsonPropertyName("match_id")]
        public string Id { get; set; }

        [JsonPropertyName("match_time")]
        public string Time { get; set; }

        [JsonPropertyName("match_hometeam_name")]
        public string Hometeam_name {get; set; }

        [JsonPropertyName("match_awayteam_name")]
        public string Awayteam_name {get; set; }
    }
}