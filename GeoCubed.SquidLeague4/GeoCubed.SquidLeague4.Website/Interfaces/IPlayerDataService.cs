using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IPlayerDataService
    {
        Task<List<PlayerDetailViewModel>> GetAllPlayers();
        Task<PlayerDetailViewModel> GetPlayerById(int id);
        Task<ApiResponse<int>> CreatePlayer(PlayerDetailViewModel playerDetailViewModel);
        Task<ApiResponse<int>> UpdatePlayer(PlayerDetailViewModel playerDetailViewModel);
        Task<ApiResponse<int>> DeletePlayer(int id);
    }
}
