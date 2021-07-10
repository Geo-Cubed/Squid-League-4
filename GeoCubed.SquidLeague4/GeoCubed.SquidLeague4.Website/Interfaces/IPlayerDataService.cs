using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IPlayerDataService
    {
        Task<List<AdminPlayerViewModel>> GetAllPlayers();
        Task<PlayerDetailViewModel> GetPlayerById(int id);
        Task<ApiResponse<int>> CreatePlayer(AdminPlayerViewModel playerDetailViewModel);
        Task<ApiResponse<int>> UpdatePlayer(AdminPlayerViewModel playerDetailViewModel);
        Task<ApiResponse<int>> DeletePlayer(int id);
        Task<List<AdminPlayerViewModel>> GetPlayersByTeamId(int teamId);
    }
}
