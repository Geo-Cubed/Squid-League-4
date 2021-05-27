using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IGameDataService
    {
        Task<List<GameViewModel>> GetAllGames();
        Task<ApiResponse<int>> CreateGame(GameViewModel gameViewModel);
        Task<ApiResponse<int>> UpdateGame(GameViewModel gameViewModel);
        Task<ApiResponse<int>> DeleteGame(int id);
    }
}
