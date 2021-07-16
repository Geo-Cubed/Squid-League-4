using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchInfo
{
    public record GetMatchInfoQuery() : IRequest<List<MatchInfoVm>>;
}