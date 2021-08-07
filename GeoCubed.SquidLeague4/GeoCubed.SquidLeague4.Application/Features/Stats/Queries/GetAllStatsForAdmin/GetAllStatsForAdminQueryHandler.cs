using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStatsForAdmin
{
    public class GetAllStatsForAdminQueryHandler : IRequestHandler<GetAllStatsForAdminQuery, List<AdminStatsVm>>
    {
        private readonly IAsyncRepository<Statistic> _statsRepository;
        private readonly IMapper _mapper;

        public GetAllStatsForAdminQueryHandler(IMapper mapper, IAsyncRepository<Statistic> statsRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statsRepository;
        }

        public async Task<List<AdminStatsVm>> Handle(GetAllStatsForAdminQuery request, CancellationToken cancellationToken)
        {
            var adminStats = await this._statsRepository.GetAllAsync();
            var mappedStats = this._mapper.Map<List<AdminStatsVm>>(adminStats);
            return mappedStats;
        }
    }
}
