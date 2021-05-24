using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId
{
    public class GetGamesByTeamIdQuery : IRequest<List<TeamGameVm>>
    {
        public int TeamId { get; set; }
    }
}