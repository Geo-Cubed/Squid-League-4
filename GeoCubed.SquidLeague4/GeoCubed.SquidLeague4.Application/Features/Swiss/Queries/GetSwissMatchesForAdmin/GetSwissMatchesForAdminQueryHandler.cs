using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesForAdmin
{
    public class GetSwissMatchesForAdminQueryHandler : IRequestHandler<GetSwissMatchesForAdminQuery, List<BracketSwissAdminVm>>
    {
        private readonly IAsyncRepository<BracketSwiss> _swissRepository;
        private readonly IMapper _mapper;

        public GetSwissMatchesForAdminQueryHandler(IMapper mapper, IAsyncRepository<BracketSwiss> swissRepository)
        {
            this._mapper = mapper;
            this._swissRepository = swissRepository;
        }

        public async Task<List<BracketSwissAdminVm>> Handle(GetSwissMatchesForAdminQuery request, CancellationToken cancellationToken)
        {
            var swissMatches = await this._swissRepository.GetAllAsync();
            var mappedMatches = this._mapper.Map<List<BracketSwissAdminVm>>(swissMatches);
            return mappedMatches;
        }
    }
}
