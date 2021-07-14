using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Commands.SaveGameInfo
{
    public class SaveGameInfoCommandHandler : IRequestHandler<SaveGameInfoCommand, SaveGameInfoCommandResponse>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly IGameSettingRepository _gameSettingRepository;
        private readonly IWeaponPlayedRepository _weaponPlayedRepository;
        private readonly IMapper _mapper;

        public SaveGameInfoCommandHandler(
            IMapper mapper, 
            IGameRepository gameRepository,
            IWeaponRepository weaponRepository,
            IPlayerRepository playerRepository,
            IMatchRepository matchRepository,
            IGameSettingRepository gameSettingRepository,
            IWeaponPlayedRepository weaponPlayedRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
            this._matchRepository = matchRepository;
            this._gameSettingRepository = gameSettingRepository;
            this._weaponPlayedRepository = weaponPlayedRepository;
        }

        public async Task<SaveGameInfoCommandResponse> Handle(SaveGameInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new SaveGameInfoCommandResponse();

            // Validate correct values.
            var valdator = new SaveGameInfoCommandValidator( 
                this._weaponRepository, 
                this._playerRepository,
                this._matchRepository,
                this._gameSettingRepository);
            var validation = await valdator.ValidateAsync(request);

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
                // Check if game exists. (if so get the id) (if not create one)
                var mappedGame = this._mapper.Map<Game>(request);
                if (await this._gameRepository.TryGetGameId(request.MatchId, request.GameSettingId, out int gameId))
                {
                    mappedGame.Id = gameId;
                    await this._gameRepository.UpdateAsync(mappedGame);
                }
                else
                {
                    // Does not exist. Therefore create game.
                    var inserted = await this._gameRepository.AddAsync(mappedGame);
                    gameId = inserted.Id;
                }

                // Home team Players.
                var homeTeamPlayers = this.GetPlayerList(
                    request.HomePlayer1,
                    request.HomePlayer2,
                    request.HomePlayer3,
                    request.HomePlayer4);

                var mappedHomeTeamPlayers = CustomMapper.ConvertToWeaponPlayed(homeTeamPlayers, gameId, true);
                if (mappedHomeTeamPlayers.Count() > 0)
                {
                    await this._weaponPlayedRepository.AddPlayersToGame(mappedHomeTeamPlayers);
                }

                // Away team Players.
                var awayTeamPlayers = this.GetPlayerList(
                    request.AwayPlayer1,
                    request.AwayPlayer2,
                    request.AwayPlayer3,
                    request.AwayPlayer4);

                var mappedAwayTeamPlayers = CustomMapper.ConvertToWeaponPlayed(awayTeamPlayers, gameId, false);
                if (mappedAwayTeamPlayers.Count() > 0)
                {
                    await this._weaponPlayedRepository.AddPlayersToGame(mappedAwayTeamPlayers);
                }
            }

            return response;
        }

        private List<BasicPlayerWeapon> GetPlayerList(params BasicPlayerWeapon[] playerArr)
        {
            return playerArr
                .Where(x => x.PlayerId > 0 && x.WeaponId > 0)
                .ToList();
        }
    }
}
