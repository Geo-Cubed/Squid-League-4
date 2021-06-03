using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IMatchDataService
    {
        Task<List<MatchDetailViewModel>> GetAllMatches();
        Task<List<AdminMatchViewModel>> GetAllMatchesForAdmin();
        Task<MatchDetailViewModel> GetMatchById(int id);
        Task<List<UpcommingMatchViewModel>> GetUpcommingMatches();
        Task<List<TeamMatchViewModel>> GetTemMatches(int teamId);
        Task<ApiResponse<int>> CreateMatch(AdminMatchViewModel matchViewModel);
        Task<ApiResponse<int>> UpdateMatch(AdminMatchViewModel matchViewModel);
        Task<ApiResponse<int>> DeleteMatch(int id);
    }
}
