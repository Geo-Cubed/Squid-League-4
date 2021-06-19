using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class BracketKnockoutDataService : BaseDataService, IBracketKnockoutDataService
    {
        private readonly IMapper _mapper;

        public BracketKnockoutDataService(IClient client, IMapper mapper, ILocalStorageService localStorage)
            : base(client, localStorage)
        {
            this._mapper = mapper;
        }

        public async Task<ApiResponse<int>> CreateKnockoutMatch(AdminKnockoutMatchViewModel knockoutMatch)
        {
            try
            {
                await this.AddBearerToken();
                var knockoutMatchCommand = this._mapper.Map<CreateKnockoutMatchCommand>(knockoutMatch);
                var newId = await this._client.AddKnockoutMatchAsync(knockoutMatchCommand);
                return new ApiResponse<int>() { Data = newId.KnockoutMatch.Id, Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<ApiResponse<int>> DeleteKnockoutMatch(int id)
        {
            try
            {
                await this.AddBearerToken();
                await this._client.DeleteKnockoutMatchAsync(id);
                return new ApiResponse<int>() { Success = true };
            }
            catch (ApiException ex)
            {
                return ConvertApiExceptions(ex);
            }
        }

        public async Task<List<AdminKnockoutMatchViewModel>> GetLowerBracketMatches()
        {
            await this.AddBearerToken();

            var lower = await this._client.GetAllLowerBracketAsync();
            var lowerMapped = this._mapper.Map<ICollection<AdminKnockoutMatchViewModel>>(lower);
            return lowerMapped.ToList();
        }

        public async Task<List<AdminKnockoutMatchViewModel>> GetUpperBracketMatches()
        {
            await this.AddBearerToken();

            var upper = await this._client.GetAllUpperBracketAsync();
            var upperMapped = this._mapper.Map<ICollection<AdminKnockoutMatchViewModel>>(upper);
            return upperMapped.ToList();
        }
    }
}
