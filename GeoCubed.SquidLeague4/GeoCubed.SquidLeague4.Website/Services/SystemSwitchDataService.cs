using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class SystemSwitchDataService : BaseDataService, ISystemSwitchDataService
    {
        private readonly IMapper _mapper;

        public SystemSwitchDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateSwitch(AdminSwitchViewModel adminSwitchViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var createSwitchCommand = this._mapper.Map<CreateSwitchCommand>(adminSwitchViewModel);
                var newId = await this._client.AddSwitchAsync(createSwitchCommand);
                return new ApiResponse<int>() { Data = newId.Switch.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteSwitch(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteSwitchAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminSwitchViewModel>> GetAllSwitchesForAdmin()
        {
            await this.AddBearerToken();
            var allSwitches = await this._client.GetAllSystemSwitchesAdminAsync();
            var mappedSwitches = this._mapper.Map<ICollection<AdminSwitchViewModel>>(allSwitches);
            return mappedSwitches.ToList();
        }

        public async Task<List<string>> GetLowerKnockoutStages()
        {
            var stages = await this._client.GetLowerStagesAsync();
            return stages.ToList();
        }

        public async Task<List<int>> GetSwissWeeks()
        {
            var swissWeeks = await this._client.GetSwissWeeksAsync();
            return swissWeeks.ToList();
        }

        public async Task<List<string>> GetUpperKnockoutStages()
        {
            var stages = await this._client.GetUpperStagesAsync();
            return stages.ToList();
        }

        public async Task<ApiResponse<int>> UpdateSwitch(AdminSwitchViewModel adminSwitchViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var updateSwitchCommand = this._mapper.Map<UpdateSwitchCommand>(adminSwitchViewModel);
                await this._client.UpdateSwitchAsync(updateSwitchCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
