using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllLowerBracket;
using GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllUpperBracket;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetSetInfo;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.UpdateGameSetting;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetGameSettingsForAdmin;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists;
using GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapsByMatchId;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Maps.Queries.GetAllMaps;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.UpdateMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetAllMatchesForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetBasicMatchInfo;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchInfo;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetTeamPlayedMatches;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList;
using GeoCubed.SquidLeague4.Application.Features.Modes.Queries.GetAllModes;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayersByTeamId;
using GeoCubed.SquidLeague4.Application.Features.Results.Commands.SaveGameInfo;
using GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList;
using GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch;
using GeoCubed.SquidLeague4.Application.Features.Switches.Commands.UpdateSwitch;
using GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwitchesForAdmin;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamList;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList;
using GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetBasicWeaponInfo;
using GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList;
using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace GeoCubed.SquidLeague4.Application.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Add maps here `CreateMap<A, B>();`
            CreateMap<Player, PlayerCommandDto>();
            CreateMap<Player, CreatePlayerCommand>().ReverseMap();
            CreateMap<Player, PlayerDetailVM>();
            CreateMap<Team, TeamDto>();
            CreateMap<Player, UpdatePlayerCommand>().ReverseMap();

            CreateMap<Team, TeamVm>();
            CreateMap<Team, TeamAdminVm>();
            CreateMap<Team, CreateTeamCommand>().ReverseMap();
            CreateMap<Team, TeamCommandDto>();
            CreateMap<Team, UpdateTeamCommand>().ReverseMap();
            CreateMap<Team, TeamWithPlayersVm>()
                .ForMember(m => m.Wins, opt => opt.MapFrom(x => 
                    x.MatchAwayTeams.Where(m => m.Winner == "away").Count() 
                    + x.MatchHomeTeams.Where(m => m.Winner == "home").Count()
                ))
                .ForMember(m => m.Losses, opt =>  opt.MapFrom(x =>
                    x.MatchAwayTeams.Where(m => m.Winner == "home").Count()
                    + x.MatchHomeTeams.Where(m => m.Winner == "away").Count()
                ));
            CreateMap<Match, TeamPlayedMatchVm>()
                .ForMember(m => m.MatchId, opt => opt.MapFrom(x => x.Id))
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));

            CreateMap<Player, PlayerDto>();
            CreateMap<Weapon, CommonWeaponDto>();

            CreateMap<HelpfulPerson, HelpfulPersonVm>();
            CreateMap<HelpfulPerson, HelpfulPersonAdminVm>();
            CreateMap<HelpfulPerson, CreateHelpfulPersonCommand>().ReverseMap();
            CreateMap<HelpfulPerson, HelpfulPersonDto>();
            CreateMap<HelpfulPerson, UpdateHelpfulPersonCommand>().ReverseMap();

            CreateMap<CasterProfile, CasterVm>();
            CreateMap<CasterProfile, CasterAdminVm>();
            CreateMap<CasterProfile, CreateCasterCommand>().ReverseMap();
            CreateMap<CasterProfile, CasterCommandDto>();
            CreateMap<CasterProfile, UpdateCasterCommand>().ReverseMap();

            CreateMap<Weapon, WeaponVm>();
            CreateMap<WeaponSub, WeaponSubDto>();
            CreateMap<WeaponSpecial, WeaponSpecialDto>();

            CreateMap<Match, UpcommingMatchDetailVm>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));
            CreateMap<Match, MatchDetailVm>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));
            CreateMap<Match, BasicMatchInfoVm>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));
            CreateMap<Match, CreateMatchCommand>().ReverseMap();
            CreateMap<Match, UpdateMatchCommand>().ReverseMap();
            CreateMap<Match, MatchCommandDto>();
            CreateMap<Match, MatchAdminVm>();
            CreateMap<Match, SwissMatchDto>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));
            
            CreateMap<BracketSwiss, SwissMatchDetailVm>();
            CreateMap<BracketSwiss, BracketSwissAdminVm>();
            CreateMap<CreateSwissMatchCommand, BracketSwiss>();
            CreateMap<BracketSwiss, SwissCommandDto>();

            CreateMap<Game, GameVm>();
            CreateMap<Game, MatchGameVm>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.Match.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.Match.AwayTeam.TeamName))
                .ForMember(m => m.Players, opt => opt.MapFrom(x => x.WeaponPlayeds));
            CreateMap<Game, TeamGameVm>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.Match.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.Match.AwayTeam.TeamName))
                .ForMember(m => m.Players, opt => opt.MapFrom(x => x.WeaponPlayeds));
            CreateMap<GameSetting, GameSettingsDto>()
                .ForMember(m => m.MapName, opt => opt.MapFrom(x => x.GameMap.MapName))
                .ForMember(m => m.MapPicture, opt => opt.MapFrom(x => x.GameMap.PicturePath))
                .ForMember(m => m.ModeName, opt => opt.MapFrom(x => x.GameMode.ModeName))
                .ForMember(m => m.ModePicture, opt => opt.MapFrom(x => x.GameMode.PicturePath));
            CreateMap<WeaponPlayed, PlayerWeaponDto>()
                .ForMember(m => m.UserName, opt => opt.MapFrom(x => x.Player.InGameName))
                .ForMember(m => m.WeaponName, opt => opt.MapFrom(x => x.Weapon.WeaponName))
                .ForMember(m => m.PicturePath, opt => opt.MapFrom(x => x.Weapon.PicturePath))
                .ForMember(m => m.IsHomeTeam, opt => opt.MapFrom(x => x.IsHomeTeam));
            CreateMap<Game, CreateGameCommand>().ReverseMap();
            CreateMap<Game, UpdateGameCommand>().ReverseMap();

            CreateMap<SystemSwitch, SystemSwitchAdminVm>();
            CreateMap<SystemSwitch, CreateSwitchCommand>().ReverseMap();
            CreateMap<SystemSwitch, SwitchCommandDto>();
            CreateMap<SystemSwitch, UpdateSwitchCommand>().ReverseMap();

            CreateMap<GameSetting, GameSettingAdminVm>();
            CreateMap<GameSetting, GameSettingCommandDto>();
            CreateMap<GameSetting, CreateGameSettingCommand>().ReverseMap();
            CreateMap<GameSetting, UpdateGameSettingCommand>().ReverseMap();
            CreateMap<GameSetting, MapListVm>()
                .ForMember(m => m.Map, opt => opt.MapFrom(x => x.GameMap))
                .ForMember(m => m.Mode, opt => opt.MapFrom(x => x.GameMode));

            CreateMap<GameMap, MapVm>();
            CreateMap<GameMap, MapListMapVm>();
            CreateMap<GameMode, ModeVm>();
            CreateMap<GameMode, MapListModeVm>();

            CreateMap<BracketKnockout, UpperBracketVm>();
            CreateMap<BracketKnockout, LowerBracketVm>();
            CreateMap<BracketKnockout, CreateKnockoutMatchCommand>().ReverseMap();
            CreateMap<BracketKnockout, BracketCommandDto>().ReverseMap();

            CreateMap<GameSetting, MatchMapListVm>()
                .ForMember(x => x.GameSettingId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.Map, opt => opt.MapFrom(x => x.GameMap))
                .ForMember(x => x.Mode, opt => opt.MapFrom(x => x.GameMode));

            CreateMap<Player, MinimumPlayerInfoVm>();
            CreateMap<Weapon, BasicWeaponInfoVm>()
                .ForMember(x => x.Name, opt => opt.MapFrom(x => x.WeaponName));

            // This is going to be a horrendous map :( .
            CreateMap<WeaponPlayed, BasicPlayerWeapon>();
            CreateMap<Game, SetInformationVm>()
                .ForMember(x => x.GameId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.SortOrder, opt => opt.MapFrom(x => x.GameSetting.SortOrder))
                .ForMember(x => x.Map, opt => opt.MapFrom(x => x.GameSetting.GameMap))
                .ForMember(x => x.Mode, opt => opt.MapFrom(x => x.GameSetting.GameMode))
                .ForMember(x => x.HomePlayer1, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == true).ToList(), 1)))
                .ForMember(x => x.HomePlayer2, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == true).ToList(), 2)))
                .ForMember(x => x.HomePlayer3, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == true).ToList(), 3)))
                .ForMember(x => x.HomePlayer4, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == true).ToList(), 4)))
                .ForMember(x => x.AwayPlayer1, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == false).ToList(), 1)))
                .ForMember(x => x.AwayPlayer2, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == false).ToList(), 2)))
                .ForMember(x => x.AwayPlayer3, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == false).ToList(), 3)))
                .ForMember(x => x.AwayPlayer4, opt => opt.MapFrom(x => this.GetWeaponPlayed(x.WeaponPlayeds.Where(wp => wp.IsHomeTeam == false).ToList(), 4)));
            CreateMap<Weapon, WeaponInfoDto>();
            CreateMap<WeaponPlayed, PlayerInfoDto>()
                .ForMember(x => x.UserName, opt => opt.MapFrom(x => x.Player.InGameName));
            CreateMap<Game, FullSetInfo>()
                .ForMember(x => x.AwayPlayers, opt => opt.MapFrom(x => x.WeaponPlayeds.Where(wp => !wp.IsHomeTeam).Take(4)))
                .ForMember(x => x.HomePlayers, opt => opt.MapFrom(x => x.WeaponPlayeds.Where(wp => wp.IsHomeTeam).Take(4)))
                .ForMember(x => x.Map, opt => opt.MapFrom(x => x.GameSetting.GameMap))
                .ForMember(x => x.Mode, opt => opt.MapFrom(x => x.GameSetting.GameMode))
                .ForMember(x => x.SortOrder, opt => opt.MapFrom(x => x.GameSetting.SortOrder));

            CreateMap<Game, SaveGameInfoCommand>().ReverseMap();
            CreateMap<Match, MatchInfoVm>()
                .ForMember(x => x.MatchId, opt => opt.MapFrom(x => x.Id))
                .ForMember(x => x.HomeTeamName, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(x => x.AwayTeamName, opt => opt.MapFrom(x => x.AwayTeam.TeamName))
                .ForMember(x => x.Caster, opt => opt.MapFrom(x => x.CasterProfile.CasterName))
                .ForMember(x => x.CoCaster, opt => opt.MapFrom(x => x.SecondaryCasterProfile.CasterName));
        }

        private WeaponPlayed GetWeaponPlayed(List<WeaponPlayed> played, int playerNum)
        {
            if (played.Count() >= playerNum)
            {
                return played.ElementAt(playerNum - 1);
            }

            return new WeaponPlayed();
        }
    }
}
