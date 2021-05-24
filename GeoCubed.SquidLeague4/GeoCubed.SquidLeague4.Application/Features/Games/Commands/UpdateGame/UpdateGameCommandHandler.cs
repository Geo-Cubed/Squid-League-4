using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame
{
    public class UpdateGameCommandHandler : IRequestHandler<UpdateGameCommand, UpdateGameCommandResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IGameSettingRepository _gameSettingRepository;
        private readonly IMapper _mapper;

        public UpdateGameCommandHandler(IMapper mapper, IGameRepository gameRepository, IMatchRepository matchRepository, IGameSettingRepository gameSettingRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
            this._matchRepository = matchRepository;
            this._gameSettingRepository = gameSettingRepository;
        }

        public async Task<UpdateGameCommandResponse> Handle(UpdateGameCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateGameCommandResponse();

            var validator = new UpdateGameCommandValidator(this._gameRepository, this._gameSettingRepository, this._matchRepository);
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
                var game = this._mapper.Map<Game>(request);
                response.Success = await this._gameRepository.UpdateAsync(game);
                if (!response.Success)
                {
                    response.Game = this._mapper.Map<GameCommandDto>(game);
                    response.Message = "There was an issue while trying to update the game";
                }
            }

            return response;
        }
    }
}
