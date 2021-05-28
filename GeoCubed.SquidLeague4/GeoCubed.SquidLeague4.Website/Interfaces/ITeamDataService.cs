using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ITeamDataService
    {
        Task<List<AdminTeamViewModel>> GetAllTeams();
        Task<List<TeamDetailViewModel>> GetAllTeamsWithPlayers();
        Task<TeamDetailViewModel> GetTeamById(int id);
        Task<ApiResponse<int>> CreateTeam(AdminTeamViewModel teamDetail);
        Task<ApiResponse<int>> UpdateTeam(AdminTeamViewModel teamDetail);
        Task<ApiResponse<int>> DeleteTeam(int id);
    }
}
