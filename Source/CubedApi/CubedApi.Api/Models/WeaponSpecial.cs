using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class WeaponSpecial
    {
        public WeaponSpecial()
        {
            Weapons = new HashSet<Weapon>();
        }

        public int Id { get; set; }
        public string SpecialName { get; set; }
        public string PicturePath { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
