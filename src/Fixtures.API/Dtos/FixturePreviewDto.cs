using System.ComponentModel.DataAnnotations;

namespace Fixtures.API
{

    public class FixturePreviewDto
    {
        public int MatchId { get; set; }

        [DataType(DataType.Time)]
        [DisplayFormat(DataFormatString = "{0:HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime Time { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        [DataType(DataType.Date), DisplayFormat( DataFormatString="{0:yyyy-MM-dd}", ApplyFormatInEditMode=true )]
        public DateTime Date { get; set; }

        public int Round { get; set; }

        public string HomeTeamBadge { get; set; }

        public string AwayTeamBadge { get; set; }
    }
}