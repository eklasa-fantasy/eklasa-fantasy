
namespace Results.API.Models
{
    public class Goalscorer : BaseEntity
    {
        public string TimeScored {get; set;} //bedzie musial zostac string z powodu bramek w doliczonym czasie (90+4)
        public string? HomeScorer {get; set;}
        public string? AwayScorer {get; set;}
        public string? HomeAssist {get; set;}
        public string? AwayAssist {get; set;}
        public string Score {get; set;}
    }
}