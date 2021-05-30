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
    public class MapDataService : BaseDataService, IMapDataService
    {
        private readonly IMapper _mapper;

        public MapDataService(IMapper mapper, IClient client, ILocalStorageService localStorageService)
            : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<List<AdminMapViewModel>> GetAllMapsForAdmin()
        {
            await this.AddBearerToken();
            var maps = await this._client.GetAllMapsAsync();
            var mappedMaps = this._mapper.Map<ICollection<AdminMapViewModel>>(maps);
            return mappedMaps.ToList();
        }
    }
}
