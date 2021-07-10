using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo
{
    public record GetSetInfoQuery(int MatchId) : IRequest<List<SetInformationVm>>
}