﻿using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.CreateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Commands.UpdateCaster;
using GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.CreateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Commands.UpdateGame;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId;
using GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.CreateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Commands.UpdateHelpfulPerson;
using GeoCubed.SquidLeague4.Application.Features.HelpfulPeople.Queries.GetHelpfulPersonById;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetUpcommingMatchesList;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Features.Swiss.Queries.GetSwissMatchesList;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.CreateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Commands.UpdateTeam;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamById;
using GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList;
using GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList;
using GeoCubed.SquidLeague4.Domain.Entities;

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
            CreateMap<Team, CreateTeamCommand>().ReverseMap();
            CreateMap<Team, TeamCommandDto>();
            CreateMap<Team, UpdateTeamCommand>().ReverseMap();
            CreateMap<Team, TeamWithPlayersVm>();
            CreateMap<Player, PlayerDto>();
            CreateMap<Weapon, CommonWeaponDto>();

            CreateMap<HelpfulPerson, HelpfulPersonVm>();
            CreateMap<HelpfulPerson, CreateHelpfulPersonCommand>().ReverseMap();
            CreateMap<HelpfulPerson, HelpfulPersonDto>();
            CreateMap<HelpfulPerson, UpdateHelpfulPersonCommand>().ReverseMap();

            CreateMap<CasterProfile, CasterVm>();
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
            CreateMap<Match, CreateMatchCommand>().ReverseMap();
            CreateMap<Match, MatchCommandDto>();

            CreateMap<BracketSwiss, SwissMatchDetailVm>();
            CreateMap<Match, SwissMatchDto>()
                .ForMember(m => m.HomeTeam, opt => opt.MapFrom(x => x.HomeTeam.TeamName))
                .ForMember(m => m.AwayTeam, opt => opt.MapFrom(x => x.AwayTeam.TeamName));

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
        }
    }
}