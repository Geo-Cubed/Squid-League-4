using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId
{
    public class GetGamesByMatchIdQuery : IRequest<List<MatchGameVm>>
    {
        public int MatchId { get; set; }
    }
}