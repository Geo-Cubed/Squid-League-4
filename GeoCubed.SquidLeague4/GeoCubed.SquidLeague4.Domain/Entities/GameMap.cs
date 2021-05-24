using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class GameMap
    {
        public GameMap()
        {
            this.GameSettings = new HashSet<GameSetting>();
        }

        public int Id { get; set; }

        public string MapName { get; set; }

        public string PicturePath { get; set; }

        public virtual ICollection<GameSetting> GameSettings { get; set; }
    }
}
