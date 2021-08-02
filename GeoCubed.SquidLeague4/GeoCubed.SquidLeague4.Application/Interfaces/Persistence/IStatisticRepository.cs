using GeoCubed.SquidLeague4.Domain.Entities;
using GeoCubed.SquidLeague4.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IStatisticRepository : IAsyncRepository<Statistic>
    {
        Task<List<StatsModel>> RunStatistic(string statisticSql);
        Task<bool> IsAliasUnique(string alias);
        Task<bool> DoesStatExist(int id);
        Task<bool> IsAliasUnique(int id, string alias);
    }
}
