using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsModifiers
{
    public record GetStatsModifiersQuery() : IRequest<StatsModifiersVm>;
}