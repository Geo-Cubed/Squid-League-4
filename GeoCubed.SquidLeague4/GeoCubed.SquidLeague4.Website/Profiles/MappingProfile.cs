﻿using AutoMapper;
using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Caster;
using GeoCubed.SquidLeague4.Website.ViewModels.GameSettings;
using GeoCubed.SquidLeague4.Website.ViewModels.KnockoutMatches;
using GeoCubed.SquidLeague4.Website.ViewModels.Matches;
using GeoCubed.SquidLeague4.Website.ViewModels.Results;
using GeoCubed.SquidLeague4.Website.ViewModels.Stats;
using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;
using System;

namespace GeoCubed.SquidLeague4.Website.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<a, b>();
            CreateMap<PlayerDetailVM, PlayerDetailViewModel>().ReverseMap();
            CreateMap<PlayerDetailVM, AdminPlayerViewModel>().ReverseMap();
            CreateMap<AdminPlayerViewModel, UpdatePlayerCommand>().ReverseMap();
            CreateMap<AdminPlayerViewModel, CreatePlayerCommand>().ReverseMap();
            CreateMap<PlayerDto, PlayerDetailViewModel>().ReverseMap();

            CreateMap<TeamVm, TeamDetailViewModel>().ReverseMap();
            CreateMap<TeamAdminVm, AdminTeamViewModel>().ReverseMap();
            CreateMap<PlayerDto, TeamPlayerViewModel>().ReverseMap();
            CreateMap<CommonWeaponDto, CommonWeaponViewModel>().ReverseMap();
            CreateMap<AdminTeamViewModel, UpdateTeamCommand>().ReverseMap();
            CreateMap<AdminTeamViewModel, CreateTeamCommand>().ReverseMap();
            CreateMap<TeamWithPlayersVm, TeamDetailViewModel>().ReverseMap();

            CreateMap<CasterVm, CasterDetailViewModel>().ReverseMap();
            CreateMap<AdminCasterViewModel, UpdateCasterCommand>().ReverseMap();
            CreateMap<AdminCasterViewModel, CreateCasterCommand>().ReverseMap();
            CreateMap<CasterAdminVm, AdminCasterViewModel>();

            CreateMap<HelpfulPersonVm, HelpfulPersonDetailViewModel>().ReverseMap();
            CreateMap<HelpfulPersonAdminVm, AdminHelpfulPeopleViewModel>().ReverseMap();
            CreateMap<AdminHelpfulPeopleViewModel, UpdateHelpfulPersonCommand>().ReverseMap();
            CreateMap<AdminHelpfulPeopleViewModel, CreateHelpfulPersonCommand>().ReverseMap();

            CreateMap<MatchDetailVm, MatchDetailViewModel>().ReverseMap();
            CreateMap<MatchAdminVm, AdminMatchViewModel>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.UtcDateTime.ConvertFromUtcToBst())).ReverseMap();
            CreateMap<AdminMatchViewModel, UpdateMatchCommand>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.ConvertFromBstToUtc())).ReverseMap();
            CreateMap<AdminMatchViewModel, CreateMatchCommand>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.ConvertFromBstToUtc())).ReverseMap();
            CreateMap<UpcommingMatchDetailVm, UpcommingMatchViewModel>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.UtcDateTime.ConvertFromUtcToBst())).ReverseMap();
            CreateMap<BasicMatchInfoVm, BasicMatchInfo>().ReverseMap();

            CreateMap<SwissMatchDetailVm, SwissMatchDetailsViewModel>();
            CreateMap<SwissMatchDto, MatchDetailsDto>();
            CreateMap<BracketSwissAdminVm, AdminBracketSwissViewModel>().ReverseMap();
            CreateMap<CreateSwissMatchCommand, AdminBracketSwissViewModel>().ReverseMap();

            CreateMap<AdminGameViewModel, GameVm>().ReverseMap();
            CreateMap<AdminGameViewModel, CreateGameCommand>().ReverseMap();
            CreateMap<AdminGameViewModel, UpdateGameCommand>().ReverseMap();
            CreateMap<TeamMatchViewModel, TeamPlayedMatchVm>().ReverseMap();
            CreateMap<TeamPlayerViewModel, PlayerWeaponDto>().ReverseMap();
            CreateMap<PlayerWeaponViewModel, PlayerWeaponDto>().ReverseMap();
            CreateMap<GameSettingViewModel, GameSettingsDto>().ReverseMap();
            CreateMap<MapListViewModel, MapListVm>().ReverseMap();
            CreateMap<MapListMapDto, MapListMapVm>().ReverseMap();
            CreateMap<MapListModeDto, MapListModeVm>().ReverseMap();

            CreateMap<AdminSwitchViewModel, SystemSwitchAdminVm>().ReverseMap();
            CreateMap<AdminSwitchViewModel, CreateSwitchCommand>().ReverseMap();
            CreateMap<AdminSwitchViewModel, UpdateSwitchCommand>().ReverseMap();

            CreateMap<AdminGameSettingViewModel, GameSettingAdminVm>().ReverseMap();
            CreateMap<AdminGameSettingViewModel, CreateGameSettingCommand>().ReverseMap();
            CreateMap<AdminGameSettingViewModel, UpdateGameSettingCommand>().ReverseMap();

            CreateMap<AdminMapViewModel, MapVm>().ReverseMap();
            CreateMap<AdminModeViewModel, ModeVm>().ReverseMap();

            CreateMap<UpperBracketVm, AdminKnockoutMatchViewModel>().ReverseMap();
            CreateMap<AdminKnockoutMatchViewModel, CreateKnockoutMatchCommand>().ReverseMap();

            CreateMap<UserDto, AdminUserViewModel>().ReverseMap();
            CreateMap<RoleRequest, AdminRoleRequestViewModel>().ReverseMap();

            CreateMap<MatchMapListVm, MatchMapListViewModel>().ReverseMap();
            CreateMap<MinimumPlayerInfoVm, AdminPlayerViewModel>().ReverseMap();
            CreateMap<BasicWeaponInfoVm, BasicWeaponInfo>().ReverseMap();
            CreateMap<BasicPlayerWeapon, AdminPlayerWeaponViewModel>().ReverseMap();
            CreateMap<SetInformationVm, AdminResultsModel>();
            CreateMap<AdminResultsModel, SaveGameInfoCommand>();
            CreateMap<PlayerInfoDto, PlayerWeaponInfoDto>()
                .ForMember(x => x.PicturePath, opt => opt.MapFrom(x => x.Weapon.PicturePath))
                .ForMember(x => x.WeaponName, opt => opt.MapFrom(x => x.Weapon.WeaponName));
            CreateMap<FullSetInfo, SetInformationViewModel>();
            CreateMap<MatchInfoVm, MatchInfoViewModel>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.UtcDateTime.ConvertFromUtcToBst())).ReverseMap();
            CreateMap<TeamVm, BasicTeamViewModel>();

            CreateMap<StatsInfoVm, StatsOptionsViewModel>().ReverseMap();
            CreateMap<AdminStatsVm, AdminStatsViewModel>().ReverseMap();
            CreateMap<AdminStatsViewModel, CreateStatsCommand>().ReverseMap();
            CreateMap<AdminStatsViewModel, UpdateStatsCommand>().ReverseMap();
            CreateMap<StatsModifiersVm, StatsModifiersViewModel>().ReverseMap();
            CreateMap<StatsDataVm, StatsDataViewModel>().ReverseMap();

            CreateMap<KnockoutMatchInfo, KnockoutInfoViewModel>().ReverseMap();
        }
    }
}
