using FluentValidation;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.SaveGameInfo
{
    internal class SaveGameInfoCommandValidator : AbstractValidator<SaveGameInfoCommand>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;

        public SaveGameInfoCommandValidator(
            IGameRepository gameRepository, 
            IWeaponRepository weaponRepository, 
            IPlayerRepository playerRepository,
            IMatchRepository matchRepository)
        {
            this._gameRepository = gameRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
            this._matchRepository = matchRepository;

            RuleFor(c => c.HomeTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Home team score must be >= 0")
                .LessThanOrEqualTo(100).WithMessage("Home team score must be <= 100")
                .NotEqual(c => c.AwayTeamScore).WithMessage("Scores cannot be equal");

            RuleFor(c => c.AwayTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Away team score must be >= 0")
                .LessThanOrEqualTo(100).WithMessage("Away team score must be <= 100");

            RuleFor(e => e)
                .MustAsync(DoesGameExist).WithMessage("The game does not exist");
        }

        private async Task<bool> DoesGameExist(SaveGameInfoCommand e, CancellationToken token)
        {
            // If game id <= 0 then it is a new game to be inserted.
            if (e.GameId <= 0)
            {
                return true;
            }

            return await this._gameRepository.DoesGameExist(e.GameId);
        }
    }
}