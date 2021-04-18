using SquidLeagueWebsite.Models.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class PlayerGame
    {
        public Game Game { get; set; }

        public Player Player { get; set; }

        public Weapon WeaponPlayed { get; set; }
    }
}
