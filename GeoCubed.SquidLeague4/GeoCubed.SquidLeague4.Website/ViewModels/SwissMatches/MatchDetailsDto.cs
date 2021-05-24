namespace GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches
{
    public class MatchDetailsDto
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeTeamScore { get; set; } = 0;

        public int AwayTeamScore { get; set; } = 0;
    }
}
