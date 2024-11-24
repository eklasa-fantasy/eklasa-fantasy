using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    public class APIGoalscorerDto
    {
        [JsonPropertyName("time")]
        public string TimeScored {get; set;} //bedzie musial zostac string z powodu bramek w doliczonym czasie (90+4)

        [JsonPropertyName("home_scorer")]
        public string HomeScorer {get; set;}

        [JsonPropertyName("away_scorer")]
        public string AwayScorer {get; set;}

        [JsonPropertyName("home_assist")]
        public string HomeAssist {get; set;}

        [JsonPropertyName("away_assist")]
        public string AwayAssist {get; set;}

        [JsonPropertyName("score")]
        public string Score {get; set;}

        


    }
}