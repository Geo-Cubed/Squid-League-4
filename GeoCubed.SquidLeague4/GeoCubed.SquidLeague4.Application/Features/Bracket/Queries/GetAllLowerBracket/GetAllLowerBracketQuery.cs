using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllLowerBracket
{
    public record GetAllLowerBracketQuery : IRequest<List<LowerBracketVm>>;
}