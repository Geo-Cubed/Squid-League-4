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

        public async Task<List<AdminLowerBracketViewModel>> GetLowerBracketMatches()
        {
            await this.AddBearerToken();

            var lower = await this._client.GetAllLowerBracketAsync();
            var lowerMapped = this._mapper.Map<ICollection<AdminLowerBracketViewModel>>(lower);
            return lowerMapped.ToList();
        }

        public async Task<List<AdminUpperBracketViewModel>> GetUpperBracketMatches()
        {
            await this.AddBearerToken();

            var upper = await this._client.GetAllUpperBracketAsync();
            var upperMapped = this._mapper.Map<ICollection<AdminUpperBracketViewModel>>(upper);
            return upperMapped.ToList();
        }
    }
}
