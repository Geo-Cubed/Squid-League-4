using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SquidLeagueAdmin.Models.Enums
{
    public enum WeaponType
    {
        [Description("Shooter")]
        Shooter,

        [Description("Blaster")]
        Blaster,

        [Description("Roller")]
        Roller,

        [Description("Brush")]
        Brush,

        [Description("Charger")]
        Charger,

        [Description("Slosher")]
        Slosher,

        [Description("Splatling")]
        Splatling,

        [Description("Dualie")]
        Dualie,

        [Description("Brella")]
        Brella
    }
}
