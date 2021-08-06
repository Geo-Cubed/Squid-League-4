using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class StatsDataService : BaseDataService, IStatsDataService
    {
        private readonly IMapper _mapper;

        public StatsDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateStats(AdminStatsViewModel adminStatsViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var createStatsCommand = this._mapper.Map<CreateStatsCommand>(adminStatsViewModel);
                await this._client.AddStatsAsync(createStatsCommand);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteStats(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteStatsAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<StatsOptionsViewModel>> GetAllStats()
        {
            var stats = await this._client.GetAllStatsAsync();
            var mappedStats = this._mapper.Map<ICollection<StatsOptionsViewModel>>(stats);
            return mappedStats.ToList();
        }

        public async Task<List<AdminStatsViewModel>> GetAllStatsForAdmin()
        {
            await this.AddBearerToken();
            var stats = await this._client.GetAllStatsForAdminAsync();
            var mappedStats = this._mapper.Map<ICollection<AdminStatsViewModel>>(stats);
            return mappedStats.ToList();
        }

        public async Task<StatsModifiersViewModel> GetStatsModifiers()
        {
            var statsModifiers = await this._client.GetStatsModifiersAsync();
            var mapped = this._mapper.Map<StatsModifiersViewModel>(statsModifiers);
            return mapped;
        }

        public async Task<ApiResponse<int>> UpdateStats(AdminStatsViewModel adminStatsViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var updateStatsCommand = this._mapper.Map<UpdateStatsCommand>(adminStatsViewModel);
                await this._client.UpdateStatsAsync(updateStatsCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
