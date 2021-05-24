using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
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

        public async Task<List<SwissMatchDetailsViewModel>> GetAllSwissMatches()
        {
            var swissMatches = await this._client.GetSwissMatchesAsync();
            var mappedMatches = this._mapper.Map<ICollection<SwissMatchDetailsViewModel>>(swissMatches);
            return mappedMatches.ToList();
        }
    }
}
