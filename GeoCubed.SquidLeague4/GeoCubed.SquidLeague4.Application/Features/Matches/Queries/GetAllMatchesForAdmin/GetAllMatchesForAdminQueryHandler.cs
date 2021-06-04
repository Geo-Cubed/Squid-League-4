using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetAllMatchesForAdmin
{
    public class GetAllMatchesForAdminQueryHandler : IRequestHandler<GetAllMatchesForAdminQuery, List<MatchAdminVm>>
    {
        private readonly IAsyncRepository<Match> _matchRepository;
        private readonly IMapper _mapper;

        public GetAllMatchesForAdminQueryHandler(IMapper mapper, IAsyncRepository<Match> matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<List<MatchAdminVm>> Handle(GetAllMatchesForAdminQuery request, CancellationToken cancellationToken)
        {
            var allMatches = await this._matchRepository.GetAllAsync();
            var mappedMatches = this._mapper.Map<List<MatchAdminVm>>(allMatches);
            return mappedMatches;
        }
    }
}
