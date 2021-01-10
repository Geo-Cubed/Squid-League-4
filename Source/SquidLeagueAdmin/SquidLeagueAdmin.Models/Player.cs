using SquidLeagueAdmin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Player
    {
        public int? Id;

        public string InGameName;

        public Ranks SplatZonesRank;

        public Ranks RainMakerRank;

        public Ranks TowerControlRank;

        public Ranks ClamBlitzRank;

        public int? TeamId;

        public int IsActive;

        public override string ToString()
        {
            return $"{this.InGameName} - {((IsActive == 1) ? "" : "Not ")} Active";
        }
    }
}
