using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class GameMode
    {
        public GameMode()
        {
            GameSettings = new HashSet<GameSetting>();
        }

        public int Id { get; set; }
        public string ModeName { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<GameSetting> GameSettings { get; set; }
    }
}
