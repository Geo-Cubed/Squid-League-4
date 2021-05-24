using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame
{
    internal class UpdateGameCommandValidator : AbstractValidator<UpdateGameCommand>
    {
        private IGameRepository _gameRepository;
        private IGameSettingRepository _gameSettingRepository;
        private IMatchRepository _matchRepository;

        public UpdateGameCommandValidator(IGameRepository gameRepository, IGameSettingRepository gameSettingRepository, IMatchRepository matchRepository)
        {
            this._gameRepository = gameRepository;
            this._gameSettingRepository = gameSettingRepository;
            this._matchRepository = matchRepository;

            RuleFor(c => c.AwayTeamScore)
                .GreaterThanOrEqualTo(0.0).WithMessage("Cannot have a negative score")
                .LessThanOrEqualTo(100.0).WithMessage("Cannot have a score greater than 100");

            RuleFor(c => c.HomeTeamScore)
                .GreaterThanOrEqualTo(0.0).WithMessage("Cannot have a negative score")
                .LessThanOrEqualTo(100.0).WithMessage("Cannot have a score greater than 100");

            RuleFor(e => e)
                .MustAsync(DoesGameExist).WithMessage("A Game does not exist with provided id")
                .MustAsync(DoesMatchExist).WithMessage("A Match does not exist with provided id")
                .MustAsync(DoesGameSettingExist).WithMessage("A Game Setting does not exist with provided id")
                .Must(DoesScoreExceed100).WithMessage("Combined score cannot be greater than 100");
        }

        private async Task<bool> DoesMatchExist(UpdateGameCommand e, CancellationToken token)
        {
            if (e.MatchId <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.MatchId);
        }

        private async Task<bool> DoesGameSettingExist(UpdateGameCommand e, CancellationToken token)
        {
            if (e.GameSettingId <= 0)
            {
                return false;
            }

            return await this._gameSettingRepository.DoesGameSettingExist(e.GameSettingId);
        }

        private async Task<bool> DoesGameExist(UpdateGameCommand e, CancellationToken token)
        {
            if (e.Id <= 0)
            {
                return false;
            }

            return await this._gameRepository.DoesGameExist(e.Id);
        }

        private bool DoesScoreExceed100(UpdateGameCommand e)
        {
            if (e.HomeTeamScore + e.AwayTeamScore > 100)
            {
                return false;
            }

            return true;
        }
    }
}