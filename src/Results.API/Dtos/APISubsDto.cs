using System.Text.Json.Serialization;

namespace Results.API.Dtos
{
    public class APISubsDto
    {

        //teoretycznie nie potrzebujemy oddzielnych Dto dla home i away, ale dla przejrzystosci dalem tak

        [JsonPropertyName("home")]
        public List<APISubsHomeDto> HomeSubs {get; set;}

        [JsonPropertyName("away")]
        public List<APISubsAwayDto> AwaySubs {get; set;}
    }
}