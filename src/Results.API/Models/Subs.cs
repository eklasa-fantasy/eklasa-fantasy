

namespace Results.API.Models
{
    public class Subs : BaseEntity
    {
        //teoretycznie nie potrzebujemy oddzielnych Dto dla home i away, ale dla przejrzystosci dalem tak
        public List<SubHome> HomeSubs {get; set;}
        public List<SubAway> AwaySubs {get; set;}
    }
}