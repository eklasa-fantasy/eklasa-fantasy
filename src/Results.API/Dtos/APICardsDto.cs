using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    public class APICardsDto
    {
        [JsonPropertyName("time")]
        public string TimeReceived {get; set;} //bedzie musial zostac string z powodu kartek w doliczonym czasie (90+4)

        [JsonPropertyName("home_fault")]
        public string? HomeFault {get; set;}

        [JsonPropertyName("away_fault")]
        public string? AwayFault {get; set;}

        [JsonPropertyName("card")]
        public string Card {get; set;}
    }
}