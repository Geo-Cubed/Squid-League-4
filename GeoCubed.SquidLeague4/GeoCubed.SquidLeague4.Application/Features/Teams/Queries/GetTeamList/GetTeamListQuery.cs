using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamList
{
    public class GetTeamListQuery : IRequest<List<TeamVm>>
    {
    }
}
