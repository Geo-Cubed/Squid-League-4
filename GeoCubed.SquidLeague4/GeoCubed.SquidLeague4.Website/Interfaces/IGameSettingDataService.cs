using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IGameSettingDataService
    {
        Task<List<AdminGameSettingViewModel>> GetGameSettingsForAdmin();
        Task<ApiResponse<int>> CreateSetting(AdminGameSettingViewModel gameSettingViewModel);
        Task<ApiResponse<int>> UpdateSetting(AdminGameSettingViewModel gameSettingViewModel);
        Task<ApiResponse<int>> DeleteSetting(int id);
        Task<List<MapListViewModel>> GetMapLists();
    }
}
