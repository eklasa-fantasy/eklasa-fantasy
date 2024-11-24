using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    
    public class APIResultDto
    {
        [JsonPropertyName("match_id")]
        public string Id { get; set; }

        [JsonPropertyName("match_time")]
        public string Time { get; set; }

        [JsonPropertyName("match_hometeam_name")]
        public string HomeTeamName {get; set; }

        [JsonPropertyName("match_awayteam_name")]
        public string AwayTeamName {get; set; }

        [JsonPropertyName("match_hometeam_id")]
        public string HomeTeamId {get; set;}

        [JsonPropertyName("match_awayteam_id")]
        public string AwayTeamId {get; set;}

        [JsonPropertyName("match_date")]
        public string Date {get; set;}

        [JsonPropertyName("match_round")]
        public string Round {get; set;}

        [JsonPropertyName("team_home_badge")]
        public string HomeTeamBadge {get; set;}

        [JsonPropertyName("team_away_badge")]
        public string AwayTeamBadge {get; set;}

        [JsonPropertyName("match_hometeam_score")]
        public string HomeTeamScore {get; set;}

        [JsonPropertyName("match_awayteam_score")]
        public string AwayTeamScore {get; set;}

        [JsonPropertyName("match_live")]
        public string isMatchLive {get; set;} //Bardzo wazne

        [JsonPropertyName("goalscorer")]
        public List<APIGoalscorerDto> GoalScorers {get; set;}




    }
}