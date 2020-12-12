using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models
{
    public class PlayerMatch
    {
        public int? MatchId { get; set; }

        public int? PlayerTeamId { get; set; }

        public int? EnemyTeamId { get; set; }

        public string PlayerTeam { get; set; }

        public string EnemyTeam { get; set; }

        public string MapPath { get; set; }

        public string MapName { get; set; }

        public string ModePath { get; set; }

        public string ModeName { get; set; }

        public string WeaponPath { get; set; }

        public string WeaponName { get; set; }

        public string PlayerTeamScore { get; set; }

        public string EnemyTeamScore { get; set; }
    }
}
