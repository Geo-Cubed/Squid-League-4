namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetTeamPlayedMatches
{
    public class TeamPlayedMatchVm
    {
        public int MatchId { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string Winner { get; set; }
    }
}