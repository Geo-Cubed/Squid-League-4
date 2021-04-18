using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueWebsite.Models.Entities
{
    public class GameSetting
    {
        public int GameMapId { get; set; }

        public Map Map { get; set; }

        public int GameModeId { get; set; }

        public Mode Mode { get; set; }

        public string BracketStage { get; set; }

        public int SortOrder { get; set; }
    }
}
