using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.DeleteGameInfo
{
    internal class DeleteGameInfoCommandValidator : AbstractValidator<DeleteGameInfoCommand>
    {
        private IGameRepository _gameRepository;

        public DeleteGameInfoCommandValidator(IGameRepository gameRepository)
        {
            this._gameRepository = gameRepository;

            RuleFor(e => e)
                .MustAsync(DoesGameExist);
        }

        private async Task<bool> DoesGameExist(DeleteGameInfoCommand e, CancellationToken token)
        {
            if (e.GameId <= 0)
            {
                return false;
            }

            return await this._gameRepository.DoesGameExist(e.GameId);
        }
    }
}