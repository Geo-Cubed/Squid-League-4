using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class TeamPlayers
    {
        public int? Id { get; set; }

        public string TeamName { get; set; }

        public int TeamWins { get; set; }

        public int TeamLosses { get; set; }

        public IEnumerable<Player> Players { get; set; }
    }
}
