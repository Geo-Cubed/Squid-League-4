using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommandHandler : IRequestHandler<CreatePlayerCommand, CreatePlayerCommandResponse>
    {
        private readonly IAsyncRepository<Player> _playerRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public CreatePlayerCommandHandler(IMapper mapper, IAsyncRepository<Player> playerRepository, ITeamRepository teamRepository)
        {
            this._mapper = mapper;
            this._playerRepository = playerRepository;
            this._teamRepository = teamRepository;
        }

        public async Task<CreatePlayerCommandResponse> Handle(CreatePlayerCommand request, CancellationToken cancellationToken)
        {
            var createPlayerCommandResponse = new CreatePlayerCommandResponse();

            request.TeamId = (request.TeamId.HasValue && request.TeamId.Value <= 0) ? null : request.TeamId;

            var validator = new CreatePlayerCommandValidator(this._teamRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
            {
                createPlayerCommandResponse.Success = false;
                createPlayerCommandResponse.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    createPlayerCommandResponse.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (createPlayerCommandResponse.Success)
            {
                var player = this._mapper.Map<Player>(request);
                player = await this._playerRepository.AddAsync(player);
                createPlayerCommandResponse.Player = this._mapper.Map<PlayerCommandDto>(player);
            }

            return createPlayerCommandResponse;
        }
    }
}
