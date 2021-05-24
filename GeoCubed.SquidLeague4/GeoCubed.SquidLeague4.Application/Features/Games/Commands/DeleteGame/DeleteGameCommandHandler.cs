using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.DeleteGame
{
    public class DeleteGameCommandHandler : IRequestHandler<DeleteGameCommand, DeleteGameCommandResponse>
    {
        private readonly IGameRepository _gameRepository;

        public DeleteGameCommandHandler(IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;
        }

        public async Task<DeleteGameCommandResponse> Handle(DeleteGameCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteGameCommandResponse();

            var validator = new DeleteGameCommandValidator(this._gameRepository);
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
                var gameToDelete = await this._gameRepository.GetByIdAsync(request.Id);
                response.Success = await this._gameRepository.DeleteAsync(gameToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue while trying to delete the Game";
                    response.GameId = request.Id;
                }
            }

            return response;
        }
    }
}
