using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.ViewModels.Stats
{
    public class StatsModifiersViewModel
    {
        public StatsModifiersViewModel()
        {
            this.Modes = new Dictionary<int, string>();
            this.Weapons = new Dictionary<int, string>();
            this.Players = new Dictionary<int, string>();
        }

        public Dictionary<int, string> Modes { get; set; }

        public Dictionary<int, string> Weapons { get; set; }

        public Dictionary<int, string> Players { get; set; }
    }
}
