using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class GameSetting
    {
        public GameSetting()
        {
            Games = new HashSet<Game>();
        }

        public int Id { get; set; }
        public int GameMapId { get; set; }
        public int GameModeId { get; set; }
        public string BracketStage { get; set; }
        public int SortOrder { get; set; }

        public virtual GameMap GameMap { get; set; }
        public virtual GameMode GameMode { get; set; }
        public virtual ICollection<Game> Games { get; set; }
    }
}
