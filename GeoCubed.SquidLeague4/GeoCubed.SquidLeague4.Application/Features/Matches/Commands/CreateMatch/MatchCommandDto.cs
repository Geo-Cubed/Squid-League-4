using System;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch
{
    public class MatchCommandDto
    {
        public int Id { get; set; }

        public int HomeTeamId { get; set; }

        public int AwayTeamId { get; set; }

        public int? HomeTeamScore { get; set; }

        public int? AwayTeamScore { get; set; }

        public int? CasterProfileId { get; set; }

        public string MatchVodLink { get; set; }

        public DateTime? MatchDate { get; set; }

        public int? SecondaryCasterProfileId { get; set; }
    }
}