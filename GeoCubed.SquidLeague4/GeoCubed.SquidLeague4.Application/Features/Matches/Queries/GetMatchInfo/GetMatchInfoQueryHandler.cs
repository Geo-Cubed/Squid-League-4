using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchInfo
{
    public class GetMatchInfoQueryHandler : IRequestHandler<GetMatchInfoQuery, List<MatchInfoVm>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetMatchInfoQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<MatchInfoVm>> Handle(GetMatchInfoQuery request, CancellationToken cancellationToken)
        {
            var matches = (await this._matchRepository.GetAllMatchesAsync()).Where(x => x.HomeTeam.TeamName != "BYE" && x.AwayTeam.TeamName != "BYE");
            var mappedMatches = this._mapper.Map<List<MatchInfoVm>>(matches);
            foreach (var match in mappedMatches)
            {
                match.Stage = await this._matchRepository.GetStage(match.MatchId);
            }

            return mappedMatches;
        }
    }
}
