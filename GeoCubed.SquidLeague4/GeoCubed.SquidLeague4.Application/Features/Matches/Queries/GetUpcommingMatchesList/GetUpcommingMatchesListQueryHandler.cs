using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList
{
    public class GetUpcommingMatchesListQueryHandler : IRequestHandler<GetUpcommingMatchesListQuery, List<UpcommingMatchDetailVm>>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetUpcommingMatchesListQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<UpcommingMatchDetailVm>> Handle(GetUpcommingMatchesListQuery request, CancellationToken cancellationToken)
        {
            var matches = await this._matchRepository.GetUpcommingMatchesAsync();
            var mappedMatches = this._mapper.Map<List<UpcommingMatchDetailVm>>(matches);
            return mappedMatches;
        }
    }
}
