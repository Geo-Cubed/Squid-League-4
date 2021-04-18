using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models.Entities
{
    public class Game
    {
        public int Id { get; set; }

        public double HomeTeamScore { get; set; }

        public double AwayTeamScore { get; set; }

        public int GameSettingId { get; set; }

        public GameSetting GameSetting { get; set; }

        public int MatchId { get; set; }

        public Match Match { get; set; }
    }
}
