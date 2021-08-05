using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Pages
{
    partial class Statistics
    {
        [Inject]
        public IStatsDataService StatisticDataService { get; set; }

        public List<StatsOptionsViewModel> StatsOptions { get; set; }
            = new List<StatsOptionsViewModel>();

        public int SelectedStatsId { get; set; }

        public StatsOptionsViewModel SelectedStats { get; set; }



        protected async override Task OnInitializedAsync()
        {
            // Do Stats stuff.
        }

        protected async Task OnStatsSelectAsync(ChangeEventArgs e)
        {
            // Load the correct stats page.
        }
    }
}
