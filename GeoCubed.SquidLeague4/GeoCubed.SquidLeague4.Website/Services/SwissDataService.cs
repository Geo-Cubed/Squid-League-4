using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class SwissDataService : BaseDataService, ISwissDataService
    {
        private readonly IMapper _mapper;

        public SwissDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateSwissMatch(AdminBracketSwissViewModel adminSwissMatchViewModel)
        {
            try
            {
                var createSwissMatchCommand = this._mapper.Map<CreateSwissMatchCommand>(adminSwissMatchViewModel);
                var newId = await this._client.AddSwissMatchAsync(createSwissMatchCommand);
                return new ApiResponse<int>() { Data = newId.SwissMatch.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteSwissMatch(int id)
        {
            try
            {
                await this._client.DeleteSwissMatchAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<SwissMatchDetailsViewModel>> GetAllSwissMatches()
        {
            var swissMatches = await this._client.GetSwissMatchesAsync();
            var mappedMatches = this._mapper.Map<ICollection<SwissMatchDetailsViewModel>>(swissMatches);
            return mappedMatches.ToList();
        }

        public async Task<List<AdminBracketSwissViewModel>> GetSwissMatchesForAdmin()
        {
            await this.AddBearerToken();
            var swissMatches = await this._client.GetAllSwissMatchesForAdminAsync();
            var mappedSwissMatches = this._mapper.Map<ICollection<AdminBracketSwissViewModel>>(swissMatches);
            return mappedSwissMatches.ToList();
        }
    }
}
