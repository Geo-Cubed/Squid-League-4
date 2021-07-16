using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Results;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IGameDataService
    {
        Task<List<AdminGameViewModel>> GetAllGames();
        Task<ApiResponse<int>> CreateGame(AdminGameViewModel gameViewModel);
        Task<ApiResponse<int>> UpdateGame(AdminGameViewModel gameViewModel);
        Task<ApiResponse<int>> DeleteGame(int id);
        Task<List<AdminResultsModel>> GetResultsInfo(int matchId);
        Task<ApiResponse<int>> SaveResultsInfo(AdminResultsModel adminResultsModel);
        Task<ApiResponse<int>> DeleteResultsInfo(int gameId);
        Task<List<SetInformationViewModel>> GetSetInformation(int matchId);
    }
}
