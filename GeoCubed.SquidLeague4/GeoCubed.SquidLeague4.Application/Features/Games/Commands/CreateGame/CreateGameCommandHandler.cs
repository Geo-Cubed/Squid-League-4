using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame
{
    public class CreateGameCommandHandler : IRequestHandler<CreateGameCommand, CreateGameCommandResponse>
    {
        private readonly IAsyncRepository<Game> _gameRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IGameSettingRepository _gameSettingRepository;
        private readonly IMapper _mapper;

        public CreateGameCommandHandler(IMapper mapper, IAsyncRepository<Game> gameRepository, IMatchRepository matchRepository, IGameSettingRepository gameSettingRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
            this._gameSettingRepository = gameSettingRepository;
            this._matchRepository = matchRepository;
        }

        public async Task<CreateGameCommandResponse> Handle(CreateGameCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateGameCommandResponse();

            var validator = new CreateGameCommandValidator(this._gameSettingRepository, this._matchRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
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
                var gameToAdd = this._mapper.Map<Game>(request);
                var game = await this._gameRepository.AddAsync(gameToAdd);
                response.Game = this._mapper.Map<GameCommandDto>(game);
            }

            return response;
        }
    }
}
