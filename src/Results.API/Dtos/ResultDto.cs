

namespace Results.API.Dtos
{
    
    public class ResultDto
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

       
        public int HomeTeamScore {get; set;}

       
        public int AwayTeamScore {get; set;}

       
        public bool isMatchLive {get; set;} //Bardzo wazne

       
        public List<GoalscorerDto>? GoalScorers {get; set;}

       
        public List<CardsDto>? Cards {get; set;}

        
        public SubsDto? Substitutions  {get; set;}




    }
}