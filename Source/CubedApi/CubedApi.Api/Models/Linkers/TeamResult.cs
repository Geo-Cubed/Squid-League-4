namespace CubedApi.Api.Models.Linkers
{
    public class TeamResult
    {
        public int MatchId { get; set; }

        public int HomeTeamScore { get; set; } = 0;

        public int AwayTeamScore { get; set; } = 0;

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }
    }
}
