

namespace Results.API.Models
{

    public class Result : BaseEntity
    {
        public int MatchId { get; set; }
        public DateTime Time { get; set; }
        public string HomeTeamName { get; set; }
        public string AwayTeamName { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public DateTime Date { get; set; }
        public int Round { get; set; }
        public string HomeTeamBadge { get; set; }
        public string AwayTeamBadge { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
        public bool isMatchLive { get; set; } //Bardzo wazne
        public List<Goalscorer>? GoalScorers { get; set; }
        public List<Card>? Cards { get; set; }
        public Subs? Substitutions { get; set; }
    }
}