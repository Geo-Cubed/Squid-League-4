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
    public class ModeDataService : BaseDataService, IModeDataService
    {
        private readonly IMapper _mapper;

        public ModeDataService(IMapper mapper, IClient client, ILocalStorageService localStorageService)
            : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<List<AdminModeViewModel>> GetAllModesForAdmin()
        {
            await this.AddBearerToken();
            var modes = await this._client.GetAllModesAsync();
            var mappedModes = this._mapper.Map<ICollection<AdminModeViewModel>>(modes);
            return mappedModes.ToList();
        }
    }
}
