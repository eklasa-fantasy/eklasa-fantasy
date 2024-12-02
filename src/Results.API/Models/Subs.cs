

namespace Results.API.Models
{
    public class Subs : BaseEntity
    {
        //teoretycznie nie potrzebujemy oddzielnych Dto dla home i away, ale dla przejrzystosci dalem tak
        public List<SubsHome> HomeSubs {get; set;}
        public List<SubsAway> AwaySubs {get; set;}
    }
}