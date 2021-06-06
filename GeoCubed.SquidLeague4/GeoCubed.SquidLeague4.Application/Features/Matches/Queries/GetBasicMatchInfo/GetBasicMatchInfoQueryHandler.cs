using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetBasicMatchInfo
{
    public class GetBasicMatchInfoQueryHandler : IRequestHandler<GetBasicMatchInfoQuery, List<BasicMatchInfoVm>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetBasicMatchInfoQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<BasicMatchInfoVm>> Handle(GetBasicMatchInfoQuery request, CancellationToken cancellationToken)
        {
            var matches = await this._matchRepository.GetAllMatchesAsync();
            var mappedMatches = this._mapper.Map<List<BasicMatchInfoVm>>(matches);
            return mappedMatches;
        }
    }
}
