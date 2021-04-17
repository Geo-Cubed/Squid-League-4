using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models.Entities
{
    public partial class WeaponSub
    {
        public WeaponSub()
        {
            Weapons = new HashSet<Weapon>();
        }

        public int Id { get; set; }
        public string SubName { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
