namespace Fixtures.API.Models{
    public class FixtureResponse
    {
        public IEnumerable<Fixture> Fixtures { get; set; }
    }

    public class Fixture //PLACEHOLDER
    {
        public int Id { get; set; }
        public string HomeTeam { get; set; }
        public string AwayTeam { get; set; }
        public string Date { get; set; }
        public string Status { get; set; }
    }
}