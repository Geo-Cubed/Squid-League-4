using SquidLeagueWebsite.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class PlayerGame
    {        
        public Game Game { get; set; }

        public Map Map { get; set; }

        public Mode Mode { get; set; }

        public Team HomeTeam { get; set; }

        public Team AwayTeam { get; set; }

        public Weapon Weapon { get; set; }

        public bool IsHomeTeam { get; set; }
    }
}
