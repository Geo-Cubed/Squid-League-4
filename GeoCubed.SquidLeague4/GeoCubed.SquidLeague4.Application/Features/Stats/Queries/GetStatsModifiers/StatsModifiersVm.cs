using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsModifiers
{
    public class StatsModifiersVm
    {
        public StatsModifiersVm()
        {
            this.Modes = new Dictionary<int, string>();
            this.Weapons = new Dictionary<int, string>();
        }

        public Dictionary<int, string> Modes { get; set; }

        public Dictionary<int, string> Weapons { get; set; }
    }
}