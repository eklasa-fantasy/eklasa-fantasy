using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    public class APISubsHomeDto
    {
        [JsonPropertyName("time")]
        public string Time {get; set;}

        [JsonPropertyName("substitution")]
        public string Substitution {get; set;}
    }
}