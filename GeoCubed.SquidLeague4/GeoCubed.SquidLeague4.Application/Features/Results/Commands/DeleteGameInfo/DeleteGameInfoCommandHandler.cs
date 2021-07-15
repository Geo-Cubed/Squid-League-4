using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.DeleteGameInfo
{
    public class DeleteGameInfoCommandHandler : IRequestHandler<DeleteGameInfoCommand, DeleteGameInfoCommandResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IWeaponPlayedRepository _weaponPlayedRepository;
        private readonly IMapper _mapper;

        public DeleteGameInfoCommandHandler(
            IMapper mapper, 
            IGameRepository gameRepository, 
            IWeaponPlayedRepository weaponPlayedRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
            this._weaponPlayedRepository = weaponPlayedRepository;
        }

        public async Task<DeleteGameInfoCommandResponse> Handle(DeleteGameInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteGameInfoCommandResponse();

            var validator = new DeleteGameInfoCommandValidator(this._gameRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
            {
                response.Success = false;
                response.Message = "There is no game with that id";
            }

            // Delete players.
            if (response.Success)
            {
                response.Success = await this._weaponPlayedRepository.DeletePlayersByGameId(request.GameId);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the player weapons";
                }
            }

            // Delete the game.
            if (response.Success)
            {
                var gameToDelete = await this._gameRepository.GetByIdAsync(request.GameId);
                response.Success = await this._gameRepository.DeleteAsync(gameToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the game";
                }
            }

            return response;
        }
    }
}
