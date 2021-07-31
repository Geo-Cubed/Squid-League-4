using GeoCubed.SquidLeague4.Website.Interfaces;
using Microsoft.AspNetCore.Components;

namespace GeoCubed.SquidLeague4.Website.Pages.Admin
{
    public partial class AdminStatistics
    {
        [Inject]
        private IStatsDataService statsDataService { get; set; }
    }
}
