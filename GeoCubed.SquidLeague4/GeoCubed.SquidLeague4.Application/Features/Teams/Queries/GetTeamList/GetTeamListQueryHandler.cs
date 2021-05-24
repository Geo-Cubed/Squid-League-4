using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamList
{
    public class GetTeamListQueryHandler : IRequestHandler<GetTeamListQuery, List<TeamVm>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamListQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._teamRepository = teamRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));
        }

        public async Task<List<TeamVm>> Handle(GetTeamListQuery request, CancellationToken cancellationToken)
        {
            var teams = await this._teamRepository.GetAllAsync();
            return this._mapper.Map<List<TeamVm>>(teams);
        }
    }
}
