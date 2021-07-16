using System;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchInfo
{
    public class MatchInfoVm
    {
        public int MatchId { get; set; }

        public DateTime MatchDate { get; set; }

        public string VodLink { get; set; }

        public string HomeTeamName { get; set; }

        public string AwayTeamName { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public string Winner { get; set; }
    }
}