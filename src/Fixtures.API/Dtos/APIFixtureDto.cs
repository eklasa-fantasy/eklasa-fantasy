using System.Text.Json.Serialization;

namespace Fixtures.API.Dtos
{
    
    public class APIFixtureDto
    {
        [JsonPropertyName("match_id")]
        public int Id { get; set; }

        [JsonPropertyName("match_time")]
        public DateTime Time { get; set; }

        [JsonPropertyName("match_hometeam_name")]
        public string HomeTeamName {get; set; }

        [JsonPropertyName("match_awayteam_name")]
        public string AwayTeamName {get; set; }

        [JsonPropertyName("match_hometeam_id")]
        public int HomeTeamId {get; set;}

        [JsonPropertyName("match_hometeam_id")]
        public int AwayTeamId {get; set;}

        [JsonPropertyName("match_date")]
        public DateTime Date {get; set;}

        [JsonPropertyName("match_round")]
        public int Round {get; set;}

        [JsonPropertyName("team_home_badge")]
        public string HomeTeamBadge {get; set;}

        [JsonPropertyName("team_away_badge")]
        public string AwayTeamBadge {get; set;}

    }
}