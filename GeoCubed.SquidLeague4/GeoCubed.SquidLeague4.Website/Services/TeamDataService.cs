using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class TeamDataService : BaseDataService, ITeamDataService
    {
        private readonly IMapper _mapper;

        public TeamDataService(IClient client, IMapper mapper, ILocalStorageService localStorageService) 
            : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateTeam(AdminTeamViewModel teamDetail)
        {
            try
            {
                await this.AddBearerToken();
                var createTeamCommand = this._mapper.Map<CreateTeamCommand>(teamDetail);
                var newId = await this._client.AddTeamAsync(createTeamCommand);
                return new ApiResponse<int>() { Data = newId.Team.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteTeam(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteTeamAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminTeamViewModel>> GetAllTeamsForAdmin()
        {
            await this.AddBearerToken();
            var allTeams = await this._client.GetAllTeamsAsync();
            var mappedTeams = this._mapper.Map<ICollection<AdminTeamViewModel>>(allTeams);
            return mappedTeams.ToList();
        }

        public async Task<List<TeamDetailViewModel>> GetAllTeamsWithPlayers()
        {
            var allTeams = await this._client.GetAllTeamsAndPlayersAsync();
            var mappedTeams = this._mapper.Map<ICollection<TeamDetailViewModel>>(allTeams);
            return mappedTeams.ToList();
        }

        public async Task<TeamDetailViewModel> GetTeamById(int id)
        {
            var selectedTeam = await this._client.GetTeamByIdAsync(id);
            var mappedTeam = this._mapper.Map<TeamDetailViewModel>(selectedTeam);
            return mappedTeam;
        }

        public async Task<ApiResponse<int>> UpdateTeam(AdminTeamViewModel teamDetail)
        {
            try
            {
                await this.AddBearerToken();
                var updateTeamCommand = this._mapper.Map<UpdateTeamCommand>(teamDetail);
                await this._client.UpdateTeamAsync(updateTeamCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
