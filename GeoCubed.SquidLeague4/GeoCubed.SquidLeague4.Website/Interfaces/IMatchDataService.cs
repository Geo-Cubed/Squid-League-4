using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IMatchDataService
    {
        Task<List<MatchDetailViewModel>> GetAllMatches();
        Task<MatchDetailViewModel> GetMatchById(int id);
        Task<List<UpcommingMatchViewModel>> GetUpcommingMatches();
        Task<ApiResponse<int>> CreateMatch(MatchDetailViewModel matchDetailViewModel);
        Task<ApiResponse<int>> UpdateMatch(MatchDetailViewModel matchPersonDetailViewModel);
        Task<ApiResponse<int>> DeleteMatch(int id);
    }
}
