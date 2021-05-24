using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId
{
    public class MatchGameVm
    {
        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public GameSettingsDto GameSetting { get; set; }

        public List<PlayerWeaponDto> Players { get; set; }
    }
}