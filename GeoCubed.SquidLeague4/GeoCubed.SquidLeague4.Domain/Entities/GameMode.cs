using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class GameMode
    {
        public GameMode()
        {
            this.GameSettings = new HashSet<GameSetting>();
        }

        public int Id { get; set; }

        public string ModeName { get; set; }

        public string PicturePath { get; set; }

        public virtual ICollection<GameSetting> GameSettings { get; set; }
    }
}
