using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Caster;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class CasterDataService : BaseDataService, ICasterDataService
    {
        private readonly IMapper _mapper;

        public CasterDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base (client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateCaster(AdminCasterViewModel casterDetailViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var createCasterCommand = this._mapper.Map<CreateCasterCommand>(casterDetailViewModel);
                var newId = await this._client.AddCasterAsync(createCasterCommand);
                return new ApiResponse<int>() { Data = newId.Caster.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteCaster(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteCasterAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<CasterDetailViewModel>> GetAllCasters()
        {
            var allCasters = await this._client.GetAllCastersAsync();
            var mappedCasters = this._mapper.Map<ICollection<CasterDetailViewModel>>(allCasters);
            return mappedCasters.ToList();
        }

        public async Task<List<AdminCasterViewModel>> GetAllCastersAdmin()
        {
            await this.AddBearerToken();

            var casters = await this._client.GetAllCastersForAdminAsync();
            var mappedCasters = this._mapper.Map<ICollection<AdminCasterViewModel>>(casters);
            return mappedCasters.ToList();
        }

        public async Task<CasterDetailViewModel> GetCasterById(int id)
        {
            var selectedCaster = await this._client.GetCasterByIdAsync(id);
            var mappedPlayer = this._mapper.Map<CasterDetailViewModel>(selectedCaster);
            return mappedPlayer;
        }

        public async Task<ApiResponse<int>> UpdateCaster(AdminCasterViewModel casterDetailViewModel)
        {
            try
            {
                await this.AddBearerToken();
                var updateCasterCommand = this._mapper.Map<UpdateCasterCommand>(casterDetailViewModel);
                await this._client.UpdateCasterAsync(updateCasterCommand);
                return new ApiResponse<int> { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }
    }
}
