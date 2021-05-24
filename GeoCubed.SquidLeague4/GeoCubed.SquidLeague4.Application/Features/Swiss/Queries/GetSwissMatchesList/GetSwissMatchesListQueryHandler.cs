using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList
{
    public class GetSwissMatchesListQueryHandler : IRequestHandler<GetSwissMatchesListQuery, List<SwissMatchDetailVm>>
    {
        private readonly ISwissMatchRepository _swissMatchRepository;
        private readonly IMapper _mapper;

        public GetSwissMatchesListQueryHandler(IMapper mapper, ISwissMatchRepository swissMatchRepository)
        {
            this._mapper = mapper;
            this._swissMatchRepository = swissMatchRepository;
        }

        public async Task<List<SwissMatchDetailVm>> Handle(GetSwissMatchesListQuery request, CancellationToken cancellationToken)
        {
            var matches = await this._swissMatchRepository.GetAllSwissMatchesWithMatches();
            var mappedMatches = this._mapper.Map<List<SwissMatchDetailVm>>(matches);
            return mappedMatches;
        }
    }
}
