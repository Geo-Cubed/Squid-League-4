using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayersByTeamId
{
    public record GetPlayersByTeamIdQuery(int TeamId) : IRequest<List<MinimumPlayerInfoVm>>;
}
