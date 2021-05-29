using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ISystemSwitchDataService
    {
        Task<List<AdminSwitchViewModel>> GetAllSwitchesForAdmin();
        Task<ApiResponse<int>> CreateSwitch(AdminSwitchViewModel adminSwitchViewModel);
        Task<ApiResponse<int>> UpdateSwitch(AdminSwitchViewModel adminSwitchViewModel);
        Task<ApiResponse<int>> DeleteSwitch(int id);
    }
}
