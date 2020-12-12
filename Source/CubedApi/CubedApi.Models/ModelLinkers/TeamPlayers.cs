using CubedApi.Models.DatabaseTables;
using System.Collections.Generic;

namespace CubedApi.Models.ModelLinkers
{
    public class TeamPlayers
    {
        public TeamPlayers()
        {
            this.Players = new List<Player>();
        }

        public int? Id { get; set; }

        public string TeamName { get; set; }

        public int? TeamWins { get; set; }

        public int? TeamLosses { get; set; }

        public List<Player> Players { get; set; }
    }
}
