using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Exceptions;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById
{
    public class GetTeamByIdQueryHandler : IRequestHandler<GetTeamByIdQuery, TeamVm>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public GetTeamByIdQueryHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            this._mapper = mapper ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._teamRepository = teamRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));
        }

        public async Task<TeamVm> Handle(GetTeamByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Invalid request id.");
            }

            var team = await this._teamRepository.GetByIdAsync(request.Id);
            if (team == null)
            {
                throw new NotFoundException("Team", request.Id);
            }

            return this._mapper.Map<TeamVm>(team);
        }
    }
}
