using AutoMapper;
using CubedApi.Api.Common.Utilities.Interfaces;
using CubedApi.Api.Data;
using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Entities;
using System;
using System.Linq;

namespace CubedApi.Api.Common.Utilities
{
    public class EntityDtoConverter : IMapping
    {
        private readonly IMapper _mapper;
        private readonly SquidLeagueContext _context;

        public EntityDtoConverter(SquidLeagueContext context)
        {
            if (context == null)
            {
                throw new ArgumentException("Context cannot be null.");
            }

            this._context = context;

            var config = new MapperConfiguration(cfg => {
                cfg.CreateMap<Player, PlayerDto>()
                    .ForMember(
                        p => p.CommonWeapons, 
                        opt => opt.MapFrom(p => 
                            p.GetCommonWeapons(this._context)
                            .Select(w => this.WeaponEntityToDto(w))
                        )
                    );
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

            this._mapper = config.CreateMapper();
        }

        public PlayerDto PlayerEntityToDto(Player player)
        {
            return this._mapper.Map<Player, PlayerDto>(player);
        }

        public BracketKnockoutDto BracketKnockoutEntityToDto(BracketKnockout bracket)
        {
            return this._mapper.Map<BracketKnockout, BracketKnockoutDto>(bracket);
        }

        public BracketSwissDto BracketSwissEntityToDto(BracketSwiss bracket)
        {
            return this._mapper.Map<BracketSwiss, BracketSwissDto>(bracket);
        }

        public CasterProfileDto CasterProfileEntityToDto(CasterProfile caster)
        {
            return this._mapper.Map<CasterProfile, CasterProfileDto>(caster);
        }

        public GameDto GameEntityToDto(Game game)
        {
            return this._mapper.Map<Game, GameDto>(game);
        }

        public GameMapDto GameMapEntityToDto(GameMap gameMap)
        {
            return this._mapper.Map<GameMap, GameMapDto>(gameMap);
        }

        public GameModeDto GameModeEntityToDto(GameMode gameMode)
        {
            return this._mapper.Map<GameMode, GameModeDto>(gameMode);
        }

        public GameSettingDto GameSettingEntityToDto(GameSetting gameSetting)
        {
            return this._mapper.Map<GameSetting, GameSettingDto>(gameSetting);
        }

        public HelpfulPersonDto HelpfulPersonToDto(HelpfulPerson person)
        {
            return this._mapper.Map<HelpfulPerson, HelpfulPersonDto>(person);
        }

        public MatchDto MatchEntityToDto(Match match)
        {
            return this._mapper.Map<Match, MatchDto>(match);
        }

        public SystemSwitchDto SystemSwitchEntityToDto(SystemSwitch sysSwitch)
        {
            return this._mapper.Map<SystemSwitch, SystemSwitchDto>(sysSwitch);
        }

        public TeamDto TeamEntityToDto(Team team)
        {
            return this._mapper.Map<Team, TeamDto>(team);
        }

        public WeaponDto WeaponEntityToDto(Weapon weapon)
        {
            return this._mapper.Map<Weapon, WeaponDto>(weapon);
        }

        public WeaponPlayedDto WeaponPlayedEntityToDto(WeaponPlayed weapon)
        {
            return this._mapper.Map<WeaponPlayed, WeaponPlayedDto>(weapon);
        }

        public WeaponSpecialDto WeaponSpecialEntityToDto(WeaponSpecial weapon)
        {
            return this._mapper.Map<WeaponSpecial, WeaponSpecialDto>(weapon);
        }

        public WeaponSubDto WeaponSubEntityToDto(WeaponSub weapon)
        {
            return this._mapper.Map<WeaponSub, WeaponSubDto>(weapon);
        }
    }
}
