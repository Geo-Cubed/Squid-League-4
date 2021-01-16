using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SquidLeagueAdmin.Models.Enums
{
    public enum WeaponRole
    {
        [Description("Anchor")]
        Anchor,

        [Description("Frontline")]
        Frontline,

        [Description("Support")]
        Support,

        [Description("Midline")]
        Midline
    }
}
