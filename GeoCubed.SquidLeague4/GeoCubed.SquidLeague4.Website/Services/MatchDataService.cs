using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
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

        public async Task<ApiResponse<int>> CreateMatch(MatchDetailViewModel matchDetailViewModel)
        {
            try
            {
                var createMatchCommand = this._mapper.Map<CreateMatchCommand>(matchDetailViewModel);
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

        public async Task<MatchDetailViewModel> GetMatchById(int id)
        {
            var selectedMatch = await this._client.GetMatchByIdAsync(id);
            var mappedMatch = this._mapper.Map<MatchDetailViewModel>(selectedMatch);
            return mappedMatch;
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

        public async Task<ApiResponse<int>> UpdateMatch(MatchDetailViewModel matchDetailViewModel)
        {
            try
            {
                var updateMatchCommand = this._mapper.Map<UpdateMatchCommand>(matchDetailViewModel);
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
