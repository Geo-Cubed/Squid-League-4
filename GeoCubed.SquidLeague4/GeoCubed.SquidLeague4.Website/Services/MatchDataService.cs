using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class MatchDataService : BaseDataService, IMatchDataService
    {
        private readonly IMapper _mapper;

        public MatchDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateMatch(AdminMatchViewModel matchViewModel)
        {
            try
            {
                var createMatchCommand = this._mapper.Map<CreateMatchCommand>(matchViewModel);
                var newId = await this._client.AddMatchAsync(createMatchCommand);
                return new ApiResponse<int>() { Data = newId.Match.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteMatch(int id)
        {
            try
            {
                await this._client.DeleteMatchAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<MatchDetailViewModel>> GetAllMatches()
        {
            var allMatches = await this._client.GetAllMatchesAsync();
            var mappedMatches = this._mapper.Map<ICollection<MatchDetailViewModel>>(allMatches);
            return mappedMatches.ToList();
        }

        public async Task<List<AdminMatchViewModel>> GetAllMatchesForAdmin()
        {
            await this.AddBearerToken();
            var allMatches = await this._client.GetAllMatchesForAdminAsync();
            var mappedMatches = this._mapper.Map<ICollection<AdminMatchViewModel>>(allMatches);
            return mappedMatches.ToList();
        }

        public async Task<List<BasicMatchInfo>> GetBasicMatchInfo()
        {
            await this.AddBearerToken();
            var matches = await this._client.GetBasicMatchInfoAsync();
            var mappedMatches = this._mapper.Map<ICollection<BasicMatchInfo>>(matches);
            return mappedMatches.ToList();
        }

        public async Task<MatchDetailViewModel> GetMatchById(int id)
        {
            var selectedMatch = await this._client.GetMatchByIdAsync(id);
            var mappedMatch = this._mapper.Map<MatchDetailViewModel>(selectedMatch);
            return mappedMatch;
        }

        public async Task<List<MatchInfoViewModel>> GetMatchInfo()
        {
            var matches = await this._client.GetMatchInfoAsync();
            var mappedMatches = this._mapper.Map<List<MatchInfoViewModel>>(matches);
            return mappedMatches;
        }

        public async Task<List<TeamMatchViewModel>> GetTemMatches(int teamId)
        {
            var matches = await this._client.GetTeamPlayedMatchesAsync(teamId);
            var mappedMatches = this._mapper.Map<ICollection<TeamMatchViewModel>>(matches);
            return mappedMatches.ToList();
        }

        public async Task<List<UpcommingMatchViewModel>> GetUpcommingMatches()
        {
            var allMatches = await this._client.GetUpcommingMatchesAsync();
            var mappedMatches = this._mapper.Map<ICollection<UpcommingMatchViewModel>>(allMatches);
            return mappedMatches.ToList();
        }

        public async Task<ApiResponse<int>> UpdateMatch(AdminMatchViewModel matchViewModel)
        {
            try
            {
                var updateMatchCommand = this._mapper.Map<UpdateMatchCommand>(matchViewModel);
                await this._client.UpdateMatchAsync(updateMatchCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
