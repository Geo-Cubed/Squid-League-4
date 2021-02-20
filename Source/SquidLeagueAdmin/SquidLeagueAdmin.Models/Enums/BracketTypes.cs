using System.ComponentModel;

namespace SquidLeagueAdmin.Models.Enums
{
    public enum BracketTypes
    {
        [Description("None")]
        none,

        [Description("Swiss")]
        swiss,

        [Description("Knockout")]
        knockout
    }
}
