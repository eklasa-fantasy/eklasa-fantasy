namespace Results.API.Models
{
    public class TableTeam : BaseEntity
    {
        public int TeamId { get; set; }

        public string TeamBadge { get; set; }

        public string TeamName { get; set; }

        public int Played { get; set; }

        public int Wins { get; set; }

        public int Loses { get; set; }

        public int Draws { get; set; }

        public int Points { get; set; }

        public int GoalsF { get; set; }

        public int GoalsA { get; set; }

        public int GoalsDiff { get; set; }

    }
}