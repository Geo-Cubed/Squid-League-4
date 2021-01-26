using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace SquidLeagueAdmin.Models.Enums
{
    public enum GameModes
    {
        [Description("Undefined")]
        Undefined,

        [Description("Turf War")]
        TurfWar,

        [Description("Splat Zones")]
        SplatZones,

        [Description("Rain Maker")]
        RainMaker,

        [Description("Tower Control")]
        TowerControl,

        [Description("Clam Blitz")]
        ClamBlitz
    }
}
