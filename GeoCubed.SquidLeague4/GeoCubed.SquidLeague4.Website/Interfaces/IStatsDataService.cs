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
    }
}
