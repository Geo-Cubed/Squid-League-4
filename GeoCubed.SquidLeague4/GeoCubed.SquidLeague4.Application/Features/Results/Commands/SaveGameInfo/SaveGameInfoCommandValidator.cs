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
        private readonly IGameSettingRepository _gameSettingRepository;

        public SaveGameInfoCommandValidator(
            IGameRepository gameRepository, 
            IWeaponRepository weaponRepository, 
            IPlayerRepository playerRepository,
            IMatchRepository matchRepository,
            IGameSettingRepository gameSettingRepository)
        {
            this._gameRepository = gameRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
            this._matchRepository = matchRepository;
            this._gameSettingRepository = gameSettingRepository;

            RuleFor(c => c.HomeTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Home team score must be >= 0")
                .LessThanOrEqualTo(100).WithMessage("Home team score must be <= 100")
                .NotEqual(c => c.AwayTeamScore).WithMessage("Scores cannot be equal");

            RuleFor(c => c.AwayTeamScore)
                .GreaterThanOrEqualTo(0).WithMessage("Away team score must be >= 0")
                .LessThanOrEqualTo(100).WithMessage("Away team score must be <= 100");

            RuleFor(e => e)
                .MustAsync(DoesMatchExist).WithMessage("The match does not exist")
                .MustAsync(DoesGameSettingExist).WithMessage("The game setting does not exist")
                .MustAsync(DoPlayersExist).WithMessage("One or more players do not exist")
                .MustAsync(DoWeaponsExist).WithMessage("One or more weapons do not exist");
        }

        private async Task<bool> DoesMatchExist(SaveGameInfoCommand e, CancellationToken token)
        {
            if (e.MatchId <= 0)
            {
                return false;
            }

            return await this._matchRepository.DoesMatchExist(e.MatchId);
        }

        private async Task<bool> DoesGameSettingExist(SaveGameInfoCommand e, CancellationToken token)
        {
            if (e.GameSettingId <= 0)
            {
                return false;
            }

            return await this._gameSettingRepository.DoesGameSettingExist(e.GameSettingId);
        }

        private async Task<bool> DoPlayersExist(SaveGameInfoCommand e, CancellationToken token)
        {
            if (!await this.PlayerExist(e.HomePlayer1.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.HomePlayer2.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.HomePlayer3.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.HomePlayer4.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.AwayPlayer1.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.AwayPlayer2.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.AwayPlayer3.PlayerId))
            {
                return false;
            }

            if (!await this.PlayerExist(e.AwayPlayer4.PlayerId))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> PlayerExist(int id)
        {
            if (id <= 0)
            {
                return true;
            }

            return await this._playerRepository.DoesPlayerExist(id);
        }

        private async Task<bool> DoWeaponsExist(SaveGameInfoCommand e, CancellationToken token)
        {
            if (!await this.WeaponExist(e.HomePlayer1.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.HomePlayer2.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.HomePlayer3.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.HomePlayer4.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.AwayPlayer1.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.AwayPlayer2.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.AwayPlayer3.WeaponId))
            {
                return false;
            }

            if (!await this.WeaponExist(e.AwayPlayer4.WeaponId))
            {
                return false;
            }

            return true;
        }

        private async Task<bool> WeaponExist(int id)
        {
            if (id <= 0)
            {
                return true;
            }

            return await this._weaponRepository.DoesWeaponExist(id);
        }
    }
}