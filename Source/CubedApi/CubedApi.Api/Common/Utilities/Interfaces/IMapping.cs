using CubedApi.Api.Models.DTOs;
using CubedApi.Api.Models.Entities;

namespace CubedApi.Api.Common.Utilities.Interfaces
{
    public interface IMapping
    {
        public PlayerDto PlayerEntityToDto(Player player);

        public BracketKnockoutDto BracketKnockoutEntityToDto(BracketKnockout bracket);

        public BracketSwissDto BracketSwissEntityToDto(BracketSwiss bracket);

        public CasterProfileDto CasterProfileEntityToDto(CasterProfile caster);

        public GameDto GameEntityToDto(Game game);

        public GameMapDto GameMapEntityToDto(GameMap gameMap);

        public GameModeDto GameModeEntityToDto(GameMode gameMode);

        public GameSettingDto GameSettingEntityToDto(GameSetting gameSetting);

        public HelpfulPersonDto HelpfulPersonToDto(HelpfulPerson person);

        public MatchDto MatchEntityToDto(Match match);

        public SystemSwitchDto SystemSwitchEntityToDto(SystemSwitch sysSwitch);

        public TeamDto TeamEntityToDto(Team team);

        public WeaponDto WeaponEntityToDto(Weapon weapon);

        public WeaponPlayedDto WeaponPlayedEntityToDto(WeaponPlayed weapon);

        public WeaponSpecialDto WeaponSpecialEntityToDto(WeaponSpecial weapon);

        public WeaponSubDto WeaponSubEntityToDto(WeaponSub weapon);
    }
}
