namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList
{
    public class SwissMatchDto
    {
        public string HomeTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public string AwayTeam { get; set; }

        public int AwayTeamScore { get; set; }

        public string Winner { get; set; }
    }
}
