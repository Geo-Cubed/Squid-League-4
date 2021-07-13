using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
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
        private readonly IMapper _mapper;

        public SaveGameInfoCommandHandler(
            IMapper mapper, 
            IGameRepository gameRepository,
            IWeaponRepository weaponRepository,
            IPlayerRepository playerRepository,
            IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
            this._matchRepository = matchRepository;
        }

        public async Task<SaveGameInfoCommandResponse> Handle(SaveGameInfoCommand request, CancellationToken cancellationToken)
        {
            var response = new SaveGameInfoCommandResponse();

            // Validate correct values (correct people, game, match, weapons, score)
            var valdator = new SaveGameInfoCommandValidator(
                this._gameRepository, 
                this._weaponRepository, 
                this._playerRepository,
                this._matchRepository);

            return response;
        }
    }
}
