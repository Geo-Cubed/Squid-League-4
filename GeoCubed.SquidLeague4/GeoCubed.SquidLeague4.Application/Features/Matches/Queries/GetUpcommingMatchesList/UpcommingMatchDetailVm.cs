using System;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList
{
    public class UpcommingMatchDetailVm
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public string MatchVodLink { get; set; }

        public DateTime? MatchDate { get; set; }

        public string BracketStage { get; set; }
    }
}