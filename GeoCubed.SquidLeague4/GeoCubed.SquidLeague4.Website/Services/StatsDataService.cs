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
    }
}
