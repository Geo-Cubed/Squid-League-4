using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using System;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList
{
    public class MatchDetailVm
    {
        public int Id { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public int HomeTeamScore { get; set; }

        public int AwayTeamScore { get; set; }

        public CasterVm CasterProfile { get; set; }

        public CasterVm SecondaryCasterProdfile { get; set; }

        public DateTime? MatchDate { get; set; }

        // TODO: Add games.
    }
}