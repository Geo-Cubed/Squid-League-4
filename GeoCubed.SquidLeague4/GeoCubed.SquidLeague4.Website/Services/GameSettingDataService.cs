using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class GameSettingDataService : BaseDataService, IGameSettingDataService
    {
        private readonly IMapper _mapper;

        public GameSettingDataService(IMapper mapper, IClient client, ILocalStorageService localStorageService)
            : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateSetting(AdminGameSettingViewModel gameSettingViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var createGameSettingCommand = this._mapper.Map<CreateGameSettingCommand>(gameSettingViewModel);
                var newId = await this._client.AddGameSettingAsync(createGameSettingCommand);
                return new ApiResponse<int>() { Data = newId.GameSetting.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteSetting(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteGameSettingAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminGameSettingViewModel>> GetGameSettingsForAdmin()
        {
            await this.AddBearerToken();
            var allSettings = await this._client.GetGameSettingsForAdminAsync();
            var mappedSettings = this._mapper.Map<ICollection<AdminGameSettingViewModel>>(allSettings);
            return mappedSettings.ToList();
        }

        public async Task<List<MapListViewModel>> GetMapLists()
        {
            var maps = await this._client.GetMapListsAsync();
            var mappedMaps = this._mapper.Map<ICollection<MapListViewModel>>(maps);
            return mappedMaps.ToList();
        }

        public async Task<ApiResponse<int>> UpdateSetting(AdminGameSettingViewModel gameSettingViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var updateSettingCommand = this._mapper.Map<UpdateGameSettingCommand>(gameSettingViewModel);
                await this._client.UpdateGameSettingAsync(updateSettingCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
