

namespace Fixtures.API.Models{
  
    public class Fixture
    {
        
        public int Id {get; set;}
        public int MatchId {get; set;}

        public DateTime Time { get; set; }

        public string HomeTeamName {get; set; }

        public string AwayTeamName {get; set; }

        public int HomeTeamId {get; set;}

        public int AwayTeamId {get; set;}

        public DateTime Date {get; set;}

        public int Round {get; set;}

        public string HomeTeamBadge {get; set;}

        public string AwayTeamBadge {get; set;}
    }
}