using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class GetTeamWithPlayersListQuery : IRequest<List<TeamWithPlayersVm>>
    {
    }
}
