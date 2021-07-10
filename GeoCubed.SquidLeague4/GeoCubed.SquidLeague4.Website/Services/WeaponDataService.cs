using AutoMapper;
using Blazored.LocalStorage;
using GeoCubed.SquidLeague4.Website.Interfaces;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Services
{
    public class WeaponDataService : BaseDataService, IWeaponDataService
    {
        private readonly IMapper _mapper;

        public WeaponDataService(IClient client, IMapper mapper, ILocalStorageService localStorageService) 
            : base(client, localStorageService)
        {
            this._mapper = mapper;
        }

        public async Task<List<BasicWeaponInfo>> GetBasicWeaponInfo()
        {
            var weapons = await this._client.GetBasicWeaponInfoAsync();
            var mappedWeapons = this._mapper.Map<List<BasicWeaponInfo>>(weapons);
            return mappedWeapons;
        }
    }
}
