using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Commands.DeleteTeam
{
    public class DeleteTeamCommandHandler : IRequestHandler<DeleteTeamCommand, DeleteTeamCommandResponse>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public DeleteTeamCommandHandler(IMapper mapper, ITeamRepository teamRepository)
        {
            this._mapper = mapper ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._teamRepository = teamRepository ??
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(teamRepository.GetType(), this.GetType()));
        }

        public async Task<DeleteTeamCommandResponse> Handle(DeleteTeamCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteTeamCommandResponse();

            var validator = new DeleteTeamCommandValidator(this._teamRepository);
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
                response.TeamId = request.Id;
                var teamToDelete = await this._teamRepository.GetByIdAsync(request.Id);
                response.Success = await this._teamRepository.DeleteAsync(teamToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the team";
                }
            }

            return response;
        }
    }
}
