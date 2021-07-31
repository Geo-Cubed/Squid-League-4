using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsData
{
    public class GetStatsDataQueryHandler : IRequestHandler<GetStatsDataQuery, List<StatsDataVm>>
    {
        private readonly IStatisticRepository _statsRepository;
        private readonly IMapper _mapper;

        public GetStatsDataQueryHandler(IMapper mapper, IStatisticRepository statsRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statsRepository;
        }

        public async Task<List<StatsDataVm>> Handle(GetStatsDataQuery request, CancellationToken cancellationToken)
        {
            // TODO: sort out how to do by mode / weapon
            var statSql = await this._statsRepository.GetByIdAsync(request.statsId);
            var results = await this._statsRepository.RunStatistic(statSql.Sql);

            return null;
        }
    }
}
