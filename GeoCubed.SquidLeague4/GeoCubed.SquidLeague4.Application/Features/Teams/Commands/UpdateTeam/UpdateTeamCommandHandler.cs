using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam
{
    public class UpdateTeamCommandHandler : IRequestHandler<UpdateTeamCommand, UpdateTeamCommandResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public UpdateTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._teamRepository = teamRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));
        }

        public async Task<UpdateTeamCommandResponse> Handle(UpdateTeamCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateTeamCommandResponse();

            var validator = new UpdateTeamCommandValidator(this._teamRepository);
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
                var team = this._mapper.Map<Team>(request);
                response.Success = await this._teamRepository.UpdateAsync(team);
                if (!response.Success)
                {
                    response.Message = "There was an issue updating the team";
                    response.Team = this._mapper.Map<TeamCommandDto>(team);
                }
            }

            return response;
        }
    }
}
