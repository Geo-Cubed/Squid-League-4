using System.ComponentModel;

namespace GeoCubed.SquidLeague4.Application.Common.Enums
{
    public enum StatsModifiers
    {
        [Description("none")]
        None,

        [Description("mode")]
        Mode,

        [Description("weapon")]
        Weapon,

        [Description("team")]
        Team,

        [Description("player")]
        Player
    }
}
