using AutoMapper;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CubedApi.Api.Common.Utilities
{
    public static class EntityDtoConverter
    {
        private static IMapper _mapper;

        static EntityDtoConverter()
        {
            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Player, PlayerDto>();
                cfg.CreateMap<BracketKnockout, BracketKnockoutDto>();
                cfg.CreateMap<BracketSwiss, BracketSwissDto>();
                cfg.CreateMap<CasterProfile, CasterProfileDto>();
                cfg.CreateMap<Game, GameDto>();
                cfg.CreateMap<GameMap, GameMapDto>();
                cfg.CreateMap<GameMode, GameModeDto>();
                cfg.CreateMap<GameSetting, GameSettingDto>();
                cfg.CreateMap<HelpfulPerson, HelpfulPersonDto>();
                cfg.CreateMap<Match, MatchDto>();
                cfg.CreateMap<SystemSwitch, SystemSwitchDto>();
                cfg.CreateMap<Team, TeamDto>();
                cfg.CreateMap<Weapon, WeaponDto>();
                cfg.CreateMap<WeaponPlayed, WeaponPlayedDto>();
                cfg.CreateMap<WeaponSpecial, WeaponSpecialDto>();
                cfg.CreateMap<WeaponSub, WeaponSubDto>();
            });

            _mapper = config.CreateMapper();
        }

        public static PlayerDto PlayerEntityToDto(Player player)
        {
            return _mapper.Map<Player, PlayerDto>(player);
        }

        public static BracketKnockoutDto BracketKnockoutEntityToDto(BracketKnockout bracket)
        {
            return _mapper.Map<BracketKnockout, BracketKnockoutDto>(bracket);
        }

        public static BracketSwissDto BracketSwissEntityToDto(BracketSwiss bracket)
        {
            return _mapper.Map<BracketSwiss, BracketSwissDto>(bracket);
        }

        public static CasterProfileDto CasterProfileEntityToDto(CasterProfile caster)
        {
            return _mapper.Map<CasterProfile, CasterProfileDto>(caster);
        }

        public static GameDto GameEntityToDto(Game game)
        {
            return _mapper.Map<Game, GameDto>(game);
        }

        public static GameMapDto GameMapEntityToDto(GameMap gameMap)
        {
            return _mapper.Map<GameMap, GameMapDto>(gameMap);
        }

        public static GameModeDto GameModeEntityToDto(GameMode gameMode)
        {
            return _mapper.Map<GameMode, GameModeDto>(gameMode);
        }

        public static GameSettingDto GameSettingEntityToDto(GameSetting gameSetting)
        {
            return _mapper.Map<GameSetting, GameSettingDto>(gameSetting);
        }

        public static HelpfulPersonDto HelpfulPersonToDto(HelpfulPerson person)
        {
            return _mapper.Map<HelpfulPerson, HelpfulPersonDto>(person);
        }

        public static MatchDto MatchEntityToDto(Match match)
        {
            return _mapper.Map<Match, MatchDto>(match);
        }

        public static SystemSwitchDto SystemSwitchEntityToDto(SystemSwitch sysSwitch)
        {
            return _mapper.Map<SystemSwitch, SystemSwitchDto>(sysSwitch);
        }

        public static TeamDto TeamEntityToDto(Team team)
        {
            return _mapper.Map<Team, TeamDto>(team);
        }

        public static WeaponDto WeaponEntityToDto(Weapon weapon)
        {
            return _mapper.Map<Weapon, WeaponDto>(weapon);
        }

        public static WeaponPlayedDto WeaponPlayedEntityToDto(WeaponPlayed weapon)
        {
            return _mapper.Map<WeaponPlayed, WeaponPlayedDto>(weapon);
        }

        public static WeaponSpecialDto WeaponSpecialEntityToDto(WeaponSpecial weapon)
        {
            return _mapper.Map<WeaponSpecial, WeaponSpecialDto>(weapon);
        }

        public static WeaponSubDto WeaponSubEntityToDto(WeaponSub weapon)
        {
            return _mapper.Map<WeaponSub, WeaponSubDto>(weapon);
        }
    }
}
