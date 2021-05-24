using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList
{
    public class GetMatchListQueryHandler : IRequestHandler<GetMatchListQuery, List<MatchDetailVm>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;
        
        public GetMatchListQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<MatchDetailVm>> Handle(GetMatchListQuery request, CancellationToken cancellationToken)
        {
            var matches = await this._matchRepository.GetAllMatchesAsync();
            var mappedMatches = this._mapper.Map<List<MatchDetailVm>>(matches);
            return mappedMatches;
        }
    }
}
