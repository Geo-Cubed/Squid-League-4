using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Teams
{
    public class TeamGameViewModel
    {
        public int MatchId { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }
        
        public GameSettingViewModel GameSetting { get; set; }

        public ICollection<PlayerWeaponViewModel> Players { get; set; }
            = new List<PlayerWeaponViewModel>();
    }
}
