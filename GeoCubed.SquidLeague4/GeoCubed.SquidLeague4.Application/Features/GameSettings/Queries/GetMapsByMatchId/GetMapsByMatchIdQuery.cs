using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapsByMatchId
{
    public record GetMapsByMatchIdQuery(int MatchId) : IRequest<List<MatchMapListVm>>;
}