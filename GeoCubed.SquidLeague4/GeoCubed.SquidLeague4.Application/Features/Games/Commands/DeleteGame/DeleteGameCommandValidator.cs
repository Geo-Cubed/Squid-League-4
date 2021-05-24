using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.DeleteGame
{
    internal class DeleteGameCommandValidator : AbstractValidator<DeleteGameCommand>
    {
        private IGameRepository _gameRepository;

        public DeleteGameCommandValidator(IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;

            RuleFor(e => e)
                .MustAsync(DoesGameExist).WithMessage("A Game does not exist with the provided id");
        }

        private async Task<bool> DoesGameExist(DeleteGameCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._gameRepository.DoesGameExist(e.Id);
        }
    }
}