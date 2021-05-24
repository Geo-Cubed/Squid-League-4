using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerList
{
    public class GetPlayerListQuery : IRequest<List<PlayerDetailVM>>
    {
    }
}
