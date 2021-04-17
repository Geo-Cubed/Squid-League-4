using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class WeaponPlayed
    {
        public int Id { get; set; }
        public int PlayerId { get; set; }
        public int WeaponId { get; set; }
        public int GameId { get; set; }
        public bool IsHomeTeam { get; set; }

        public virtual Game Game { get; set; }
        public virtual Player Player { get; set; }
        public virtual Weapon Weapon { get; set; }
    }
}
