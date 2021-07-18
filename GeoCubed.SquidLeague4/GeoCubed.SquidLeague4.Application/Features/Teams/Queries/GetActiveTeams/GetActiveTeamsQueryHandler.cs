using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetActiveTeams
{
    public class GetActiveTeamsQueryHandler : IRequestHandler<GetActiveTeamsQuery, List<TeamVm>>
    {
        private readonly IMapper _mapper;
        private readonly ITeamRepository _teamRepository;

        public GetActiveTeamsQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            this._mapper = mapper;
            this._teamRepository = teamRepository;
        }

        public async Task<List<TeamVm>> Handle(GetActiveTeamsQuery request, CancellationToken cancellationToken)
        {
            var teams = (await this._teamRepository.GetAllAsync()).Where(x => x.IsActive.Value);
            var mappedTeams = this._mapper.Map<List<TeamVm>>(teams);
            return mappedTeams;
        }
    }
}
