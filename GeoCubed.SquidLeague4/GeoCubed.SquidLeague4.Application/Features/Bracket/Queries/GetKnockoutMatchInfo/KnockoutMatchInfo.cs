namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetKnockoutMatchInfo
{
    public class KnockoutMatchInfo
    {
        public int MatchId { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string Stage { get; set; }
    }
}