using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsData
{
    public record GetStatsDataQuery(int statsId, string secondaryParam) : IRequest<List<StatsDataVm>>;
}