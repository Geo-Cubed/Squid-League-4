using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Enums;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetStatsData
{
    public class GetStatsDataQueryHandler : IRequestHandler<GetStatsDataQuery, List<StatsDataVm>>
    {
        private readonly IStatisticRepository _statsRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IModeRepository _modeRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public GetStatsDataQueryHandler(
            IMapper mapper, 
            IStatisticRepository statsRepository,
            IWeaponRepository weaponRepository,
            IModeRepository modeRepository,
            IPlayerRepository playerRepository)
        {
            this._mapper = mapper;
            this._statsRepository = statsRepository;
            this._modeRepository = modeRepository;
            this._weaponRepository = weaponRepository;
            this._playerRepository = playerRepository;
        }

        public async Task<List<StatsDataVm>> Handle(GetStatsDataQuery request, CancellationToken cancellationToken)
        {
            if (request.statsId <= 0)
            {
                return null;
            }

            var statSql = await this._statsRepository.GetByIdAsync(request.statsId);
            if (statSql == null)
            {
                return null;
            }

            if (!EnumExtensions.TryGetValueFromDescription(
                    statSql.Modifier, 
                    out StatsModifiers modifier))
            {
                modifier = StatsModifiers.None;
            }

            var modifierText = string.Empty;
            switch (modifier)
            {
                case StatsModifiers.Team:
                    // Check for teams.
                    modifierText = "1 = 1";
                    break;
                case StatsModifiers.Weapon:
                    // Check for weapons.
                    modifierText = "weapon <> 0";
                    if (await this._weaponRepository.DoesWeaponExist(request.modifierId))
                    { 
                        modifierText = $"weapon = {request.modifierId}";
                    }

                    break;
                case StatsModifiers.Mode:
                    // Check for modes.
                    modifierText = "game_mode <> 0";
                    if (await this._modeRepository.DoesModeExist(request.modifierId))
                    {
                        modifierText = $"game_mode = {request.modifierId}";
                    }

                    break;
                case StatsModifiers.Player:
                    modifierText = "player_id <> 0";
                    if (await this._playerRepository.DoesPlayerExist(request.modifierId))
                    {
                        modifierText = $"player_id = {request.modifierId}";
                    }

                    break;
                case StatsModifiers.None:
                default:
                    modifierText = "1 = 1";
                    break;
            }

            var sql = statSql.Sql.Replace("@modifier", modifierText);
            var results = await this._statsRepository.RunStatistic(sql);
            var mappedResults = this._mapper.Map<List<StatsDataVm>>(results);
            return mappedResults;
        }
    }
}
