using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllUpperBracket
{
    public record GetAllUpperBracketQuery() : IRequest<List<UpperBracketVm>>;
}