using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ITeamDataService
    {
        Task<List<TeamDetailViewModel>> GetAllTeams();
        Task<List<TeamDetailViewModel>> GetAllTeamsWithPlayers();
        Task<TeamDetailViewModel> GetTeamById(int id);
        Task<ApiResponse<int>> CreateTeam(TeamDetailViewModel teamDetail);
        Task<ApiResponse<int>> UpdateTeam(TeamDetailViewModel teamDetail);
        Task<ApiResponse<int>> DeleteTeam(int id);
    }
}
