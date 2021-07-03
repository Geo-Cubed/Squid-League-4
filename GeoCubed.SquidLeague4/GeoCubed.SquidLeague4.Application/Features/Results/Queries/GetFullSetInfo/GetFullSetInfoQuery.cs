using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo
{
    public record GetFullSetInfoQuery(int MatchId) : IRequest<List<FullSetInfo>>;
}