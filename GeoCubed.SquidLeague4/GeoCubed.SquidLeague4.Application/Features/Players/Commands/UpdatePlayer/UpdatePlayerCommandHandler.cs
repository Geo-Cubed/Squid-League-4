using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommandHandler : IRequestHandler<UpdatePlayerCommand, UpdatePlayerCommandResponse>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public UpdatePlayerCommandHandler(IMapper mapper, IPlayerRepository playerRepository, ITeamRepository teamRepository)
        {
            this._mapper = mapper;
            this._playerRepository = playerRepository;
            this._teamRepository = teamRepository;
        }

        public async Task<UpdatePlayerCommandResponse> Handle(UpdatePlayerCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdatePlayerCommandResponse();

            request.TeamId = (request.TeamId.HasValue && request.TeamId.Value <= 0) ? null : request.TeamId;

            var validator = new UpdatePlayerCommandValidator(this._playerRepository, this._teamRepository);
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
                var player = this._mapper.Map<Player>(request);
                response.Success = await this._playerRepository.UpdateAsync(player);
                if (!response.Success)
                {
                    response.Message = "There was an error updating the player";
                    response.Player = this._mapper.Map<PlayerCommandDto>(player);
                }
            }

            return response;
        }
    }
}
