using SquidLeagueWebsite.Models.Entities;
using System.Collections.Generic;

namespace SquidLeagueWebsite.Models
{
    public class TeamPlayers
    {
        public Team Team { get; set; }

        public IEnumerable<Player> Players { get; set; }

        public int TeamWins { get; set; } = 0;

        public int TeamLosses { get; set; } = 0;
    }
}
