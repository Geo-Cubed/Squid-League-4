using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Teams
{
    public class TeamGames
    {
        public int MatchId { get; set; }

        public List<PlayerWeaponViewModel> HomeTeamPlayerWeapons { get; set; }

        public List<PlayerWeaponViewModel> AwayTeamPlayerWeapons { get; set; }

        public string HomeTeam { get; set; }

        public string AwayTeam { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }
        
        // TODO: Game settings - map and mode with images.
    }
}
