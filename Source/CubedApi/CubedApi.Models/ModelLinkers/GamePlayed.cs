using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Models.ModelLinkers
{
    public class GamePlayed
    {
        public GamePlayed()
        {
            this.HomeTeamComp = new List<PlayerWeapon>();
            this.AwayTeamComp = new List<PlayerWeapon>();
        }

        public string Map { get; set; }

        public string Mode { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public List<PlayerWeapon> HomeTeamComp { get; set; }

        public List<PlayerWeapon> AwayTeamComp { get; set; }
    }
}
