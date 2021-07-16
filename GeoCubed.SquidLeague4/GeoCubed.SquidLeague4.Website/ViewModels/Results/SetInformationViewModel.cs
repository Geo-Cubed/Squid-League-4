using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Results
{
    public class SetInformationViewModel
    {
        public SetInformationViewModel()
        {
            this.HomePlayers = new List<PlayerWeaponInfoDto>();
            this.AwayPlayers = new List<PlayerWeaponInfoDto>();
        }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int SortOrder { get; set; }

        public MapListMapDto Map { get; set; }

        public MapListModeDto Mode { get; set; }

        public List<PlayerWeaponInfoDto> HomePlayers { get; set; }

        public List<PlayerWeaponInfoDto> AwayPlayers { get; set; }
    }
}
