using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class GameSetting
    {
        public GameSetting()
        {
            this.Games = new HashSet<Game>();
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
