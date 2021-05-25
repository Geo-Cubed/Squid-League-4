using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetTeamPlayedMatches
{
    public class GetTeamPlayedMatchesQueryHandler : IRequestHandler<GetTeamPlayedMatchesQuery, List<TeamPlayedMatchVm>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetTeamPlayedMatchesQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<TeamPlayedMatchVm>> Handle(GetTeamPlayedMatchesQuery request, CancellationToken cancellationToken)
        {
            var matches = await this._matchRepository.GetTeamPlayedMatches(request.TeamId);
            var mappedMatches = this._mapper.Map<List<TeamPlayedMatchVm>>(matches);
            return mappedMatches;
        }
    }
}
