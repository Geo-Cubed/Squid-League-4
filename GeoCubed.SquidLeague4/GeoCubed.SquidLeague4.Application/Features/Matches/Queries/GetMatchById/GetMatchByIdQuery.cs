using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList;
using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQuery : IRequest<MatchDetailVm>
    {
        public int Id { get; set; }
    }
}