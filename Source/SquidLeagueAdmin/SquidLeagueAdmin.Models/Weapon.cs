using SquidLeagueAdmin.Models.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class Weapon
    {
        public int Id;

        public string Name;

        public string PicturePath;

        public int SubId;

        public int SpecialId;

        public WeaponType Type;

        public WeaponRole Role;

        public override string ToString()
        {
            return this.Name;
        }
    }
}
