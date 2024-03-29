﻿using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsModifiers
{
    public class GetStatsModifiersQueryHandler : IRequestHandler<GetStatsModifiersQuery, StatsModifiersVm>
    {
        private readonly IModeRepository _modeRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IPlayerRepository _playerRepository;

        public GetStatsModifiersQueryHandler(IModeRepository modeRepository, IWeaponRepository weaponRepository, IPlayerRepository playerRepository)
        {
            this._modeRepository = modeRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
        }

        public async Task<StatsModifiersVm> Handle(GetStatsModifiersQuery request, CancellationToken cancellationToken)
        {
            var statsModifiers = new StatsModifiersVm();
            var modes = (await this._modeRepository.GetAllAsync())
                    .ToDictionary(key => key.Id, value => value.ModeName);
            var weapons = (await this._weaponRepository.GetWeaponsPlayed())
                    .ToDictionary(key => key.Id, value => value.WeaponName);
            var players = (await this._playerRepository.GetPlayersWhoPlayed())
                    .ToDictionary(key => key.Id, value => value.InGameName);

            statsModifiers.Modes = modes;
            statsModifiers.Weapons = weapons;
            statsModifiers.Players = players;

            return statsModifiers;
        }
    }
}
