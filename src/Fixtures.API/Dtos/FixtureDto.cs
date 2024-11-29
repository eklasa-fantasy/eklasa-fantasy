

using System.ComponentModel.DataAnnotations;

namespace Fixtures.API.Dtos
{
    
    public class FixtureDto
    {
        public int MatchId { get; set; } 
        public string Time { get; set; }
        public string HomeTeamName {get; set; }
        public string AwayTeamName {get; set; }
        public int HomeTeamId {get; set;}
        public int AwayTeamId {get; set;}
        public string Date {get; set;}
        public int Round {get; set;}
        public string HomeTeamBadge {get; set;}
        public string AwayTeamBadge {get; set;}

    }
}