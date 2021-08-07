using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStats
{
    public class GetAllStatsQueryHandler : IRequestHandler<GetAllStatsQuery, List<StatsInfoVm>>
    {
        private readonly IMapper _mapper;
        private readonly IAsyncRepository<Statistic> _statsRepository;

        public GetAllStatsQueryHandler(IMapper mapper, IAsyncRepository<Statistic> statsRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statsRepository;
        }

        public async Task<List<StatsInfoVm>> Handle(GetAllStatsQuery request, CancellationToken cancellationToken)
        {
            var statsList = await this._statsRepository.GetAllAsync();
            var mappedStats = this._mapper.Map<List<StatsInfoVm>>(statsList);
            return mappedStats;
        }
    }
}
