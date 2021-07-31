using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStats
{
    public record GetAllStatsQuery() : IRequest<List<StatsInfoVm>>;
}