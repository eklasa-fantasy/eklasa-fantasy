

namespace Results.API.Dtos
{
    public class SubsDto
    {

        //teoretycznie nie potrzebujemy oddzielnych Dto dla home i away, ale dla przejrzystosci dalem tak

       
        public List<SubsHomeDto> HomeSubs {get; set;}

       
        public List<SubsAwayDto> AwaySubs {get; set;}
    }
}