using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsData
{
    public record GetStatsDataQuery(int statsId, int modifierId) : IRequest<List<StatsDataVm>>;
}