using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Admin
{
    public class AdminResultsModel
    {
        public int GameId { get; set; }

        public int MatchId { get; set; }

        public int GameSettingId { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public MapListMapDto Map { get; set; }

        public MapListModeDto Mode { get; set; }

        public AdminPlayerWeaponViewModel HomePlayer1 { get; set; }

        public AdminPlayerWeaponViewModel HomePlayer2 { get; set; }

        public AdminPlayerWeaponViewModel HomePlayer3 { get; set; }

        public AdminPlayerWeaponViewModel HomePlayer4 { get; set; }

        public AdminPlayerWeaponViewModel AwayPlayer1 { get; set; }

        public AdminPlayerWeaponViewModel AwayPlayer2 { get; set; }

        public AdminPlayerWeaponViewModel AwayPlayer3 { get; set; }

        public AdminPlayerWeaponViewModel AwayPlayer4 { get; set; }
    }
}
