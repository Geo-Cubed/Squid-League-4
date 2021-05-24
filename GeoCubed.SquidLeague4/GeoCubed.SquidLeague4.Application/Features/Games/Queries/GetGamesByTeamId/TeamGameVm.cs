using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId
{
    public class TeamGameVm
    {
        public int MatchId { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public GameSettingsDto GameSetting { get; set; }

        public List<PlayerWeaponDto> Players { get; set; }
    }
}