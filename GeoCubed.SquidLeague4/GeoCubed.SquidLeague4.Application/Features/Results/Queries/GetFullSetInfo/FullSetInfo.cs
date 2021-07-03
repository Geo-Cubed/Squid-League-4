using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo
{
    public class FullSetInfo
    {
        public FullSetInfo()
        {
            this.HomePlayers = new List<PlayerInfoDto>();
            this.AwayPlayers = new List<PlayerInfoDto>();
        }

        public int GameId { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public List<PlayerInfoDto> HomePlayers { get; set; }

        public List<PlayerInfoDto> AwayPlayers { get; set; }
    }
}