using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IStatsDataService
    {
        Task<List<StatsOptionsViewModel>> GetAllStats();
        Task<List<AdminStatsViewModel>> GetAllStatsForAdmin();
        Task<ApiResponse<int>> CreateStats(AdminStatsViewModel adminStatsViewModel);
        Task<ApiResponse<int>> UpdateStats(AdminStatsViewModel adminStatsViewModel);
        Task<ApiResponse<int>> DeleteStats(int id);
    }
}
