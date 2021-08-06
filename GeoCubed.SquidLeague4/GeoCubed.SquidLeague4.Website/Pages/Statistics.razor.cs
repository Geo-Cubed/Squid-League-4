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
        private IStatsDataService StatisticDataService { get; set; }

        protected List<StatsOptionsViewModel> StatsOptions { get; set; }
            = new List<StatsOptionsViewModel>();

        protected int SelectedStatsId { get; set; }

        protected StatsOptionsViewModel SelectedStats { get; set; }

        protected StatsModifiersViewModel statsModifiersVm;

        protected List<StatsDataViewModel> statsData { get; set; }
            = new List<StatsDataViewModel>();

        protected async override Task OnInitializedAsync()
        {
            // Do Stats stuff.
            this.StatsOptions = await this.StatisticDataService.GetAllStats();
            this.statsModifiersVm = await this.StatisticDataService.GetStatsModifiers();
            this.SelectedStatsId = 0;
            this.SelectedStats = null;
        }

        protected async Task OnStatsSelectAsync(ChangeEventArgs e)
        {
            // Load the correct stats page.
            if (!int.TryParse(e.Value.ToString(), out int statsId))
            {
                this.SelectedStats = null;
            }

            this.SelectedStatsId = statsId;
            this.SelectedStats = this.StatsOptions.FirstOrDefault(x => x.Id == statsId);
        }

        protected async Task OnModifierSelectAsync(ChangeEventArgs e)
        {
            // Load stats
            if (!int.TryParse(e.Value.ToString(), out int modifierId))
            {
                return;
            }

            // 1. Call stats function
            var data = this.StatisticDataService.GetStatsData(this.SelectedStatsId, modifierId);
            // 2. Order data (need to evaluate strong type)
            //this.statsData = data.OrderByDescending(x => x.Value);
            // 3. IDK maybe make sure the graph appears and titles are ok.
        }
    }
}
