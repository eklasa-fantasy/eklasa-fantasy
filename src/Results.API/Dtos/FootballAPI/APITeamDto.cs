using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    public class APITeamDto
    {
        [JsonPropertyName("team_key")]
        public string TeamId {get; set;}

        [JsonPropertyName("team_name")]
        public string TeamName {get; set;}

        [JsonPropertyName("team_badge")]
        public string TeamBadge {get; set;}
    }
}