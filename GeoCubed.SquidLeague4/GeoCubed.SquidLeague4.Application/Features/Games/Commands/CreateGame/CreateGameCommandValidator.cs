using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame
{
    internal class CreateGameCommandValidator : AbstractValidator<CreateGameCommand>
    {
        private IGameSettingRepository _gameSettingRepository;
        private IMatchRepository _matchRepository;

        public CreateGameCommandValidator(IGameSettingRepository gameSettingRepository, IMatchRepository matchRepository)
        {
            this._gameSettingRepository = gameSettingRepository;
            this._matchRepository = matchRepository;

            RuleFor(c => c.AwayTeamScore)
                .GreaterThanOrEqualTo(0.0).WithMessage("Cannot have a negative score")
                .LessThanOrEqualTo(100.0).WithMessage("Cannot have a score greater than 100");

            RuleFor(c => c.HomeTeamScore)
                .GreaterThanOrEqualTo(0.0).WithMessage("Cannot have a negative score")
                .LessThanOrEqualTo(100.0).WithMessage("Cannot have a score greater than 100");

            RuleFor(e => e)
                .MustAsync(DoesMatchExist).WithMessage("A Match does not exist with provided id")
                .MustAsync(DoesGameSettingExist).WithMessage("A Game Setting does not exist with provided id")
                .Must(DoesScoreExceed100).WithMessage("Combined score cannot be greater than 100");
        }

        private async Task<bool> DoesMatchExist(CreateGameCommand e, CancellationToken token)
        {
            if (e.MatchId <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.MatchId);
        }

        private async Task<bool> DoesGameSettingExist(CreateGameCommand e, CancellationToken token)
        {
            if (e.GameSettingId <= 0)
            {
                return false;
            }

            return await this._gameSettingRepository.DoesGameSettingExist(e.GameSettingId);
        }

        private bool DoesScoreExceed100(CreateGameCommand e)
        {
            if (e.HomeTeamScore + e.AwayTeamScore > 100)
            {
                return false;
            }

            return true;
        }
    }
}