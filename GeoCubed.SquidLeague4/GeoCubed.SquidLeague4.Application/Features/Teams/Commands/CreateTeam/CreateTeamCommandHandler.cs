using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam
{
    public class CreateTeamCommandHandler : IRequestHandler<CreateTeamCommand, CreateTeamCommandResponse>
    {
        private readonly IAsyncRepository<Team> _teamRepository;
        private readonly IMapper _mapper;

        public CreateTeamCommandHandler(IMapper mapper, IAsyncRepository<Team> teamRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._teamRepository = teamRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));
        }

        public async Task<CreateTeamCommandResponse> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateTeamCommandResponse();

            var validator = new CreateTeamCommandValidator();
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var teamEntity = this._mapper.Map<Team>(request);
                var team = await this._teamRepository.AddAsync(teamEntity);
                response.Team = this._mapper.Map<TeamCommandDto>(team);
            }

            return response;
        }
    }
}
