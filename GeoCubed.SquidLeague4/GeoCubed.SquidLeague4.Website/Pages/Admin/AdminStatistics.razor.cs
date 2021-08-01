using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminStatistics
    {
        [Inject]
        private IStatsDataService statsDataService { get; set; }

        protected List<AdminStatsViewModel> allStats { get; set; }
            = new List<AdminStatsViewModel>();

        protected int selectedStatsId { get; set; }
    }
}
