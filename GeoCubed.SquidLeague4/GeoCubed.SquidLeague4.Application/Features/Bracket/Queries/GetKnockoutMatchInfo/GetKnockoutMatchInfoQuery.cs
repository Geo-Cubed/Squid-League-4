using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetKnockoutMatchInfo
{
    public record GetKnockoutMatchInfoQuery(bool isUpper) : IRequest<List<KnockoutMatchInfo>>;
}