using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public partial class Weapon
    {
        public Weapon()
        {
            this.WeaponPlayeds = new HashSet<WeaponPlayed>();
        }

        public int Id { get; set; }

        public string WeaponName { get; set; }

        public string PicturePath { get; set; }

        public int SubId { get; set; }

        public int SpecialId { get; set; }

        public string WeaponType { get; set; }

        public string WeaponRole { get; set; }

        public virtual WeaponSpecial WeaponSpecial { get; set; }

        public virtual WeaponSub WeaponSub { get; set; }

        public virtual ICollection<WeaponPlayed> WeaponPlayeds { get; set; }
    }
}
