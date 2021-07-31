using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class StatisticRepository : BaseRepository<Statistic>, IStatisticRepository
    {
        public StatisticRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<List<StatsModel>> RunStatistic(string statisticSql)
        {
            var result = new List<StatsModel>();
            try
            {
                result = this._dbContext.StatsModel
                    .FromSqlRaw(statisticSql)
                    .ToList();
            }
            catch
            {
                // 🤷 idk.
                result = null;
            }

            return Task.FromResult(result);
        }
    }
}
