using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class Weapon
    {
        public Weapon()
        {
            WeaponPlayeds = new HashSet<WeaponPlayed>();
        }

        public int Id { get; set; }
        public string WeaponName { get; set; }
        public string PicturePath { get; set; }
        public int SubId { get; set; }
        public int SpecialId { get; set; }
        public string WeaponType { get; set; }
        public string WeaponRole { get; set; }

        public virtual WeaponSpecial Special { get; set; }
        public virtual WeaponSub Sub { get; set; }
        public virtual ICollection<WeaponPlayed> WeaponPlayeds { get; set; }
    }
}
