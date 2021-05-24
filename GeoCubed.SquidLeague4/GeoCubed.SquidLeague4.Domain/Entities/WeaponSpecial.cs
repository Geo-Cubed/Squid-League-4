using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Domain.Entities
{
    public class WeaponSpecial
    {
        public WeaponSpecial()
        {
            this.Weapons = new HashSet<Weapon>();
        }

        public int Id { get; set; }

        public string SpecialName { get; set; }

        public string PicturePath { get; set; }

        public virtual ICollection<Weapon> Weapons { get; set; }
    }
}
