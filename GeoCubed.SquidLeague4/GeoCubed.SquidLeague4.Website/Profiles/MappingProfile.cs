using AutoMapper;
using GeoCubed.SquidLeague4.Website.Common.Helpers;
using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using GeoCubed.SquidLeague4.Website.ViewModels.Teams;

namespace GeoCubed.SquidLeague4.Website.Profiles
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // CreateMap<a, b>();
            CreateMap<PlayerDetailVM, PlayerDetailViewModel>().ReverseMap();
            CreateMap<PlayerDetailViewModel, UpdatePlayerCommand>().ReverseMap();
            CreateMap<PlayerDetailViewModel, CreatePlayerCommand>().ReverseMap();
            CreateMap<TeamDto, TeamDetailViewModel>().ReverseMap();
            CreateMap<PlayerDto, PlayerDetailViewModel>().ReverseMap();

            CreateMap<TeamVm, TeamDetailViewModel>().ReverseMap();
            CreateMap<PlayerDto, TeamPlayerViewModel>().ReverseMap();
            CreateMap<CommonWeaponDto, CommonWeaponViewModel>().ReverseMap();
            CreateMap<TeamDetailViewModel, UpdateTeamCommand>().ReverseMap();
            CreateMap<TeamDetailViewModel, CreateTeamCommand>().ReverseMap();
            CreateMap<TeamWithPlayersVm, TeamDetailViewModel>().ReverseMap();

            CreateMap<CasterVm, CasterDetailViewModel>().ReverseMap();
            CreateMap<CasterDetailViewModel, UpdateCasterCommand>().ReverseMap();
            CreateMap<CasterDetailViewModel, CreateCasterCommand>().ReverseMap();

            CreateMap<HelpfulPersonVm, HelpfulPersonDetailViewModel>().ReverseMap();
            CreateMap<HelpfulPersonDetailViewModel, UpdateHelpfulPersonCommand>().ReverseMap();
            CreateMap<HelpfulPersonDetailViewModel, CreateHelpfulPersonCommand>().ReverseMap();

            CreateMap<MatchDetailVm, MatchDetailViewModel>().ReverseMap();
            CreateMap<MatchDetailViewModel, UpdateMatchCommand>().ReverseMap();
            CreateMap<MatchDetailViewModel, CreateMatchCommand>().ReverseMap();
            CreateMap<UpcommingMatchDetailVm, UpcommingMatchViewModel>()
                .ForMember(m => m.MatchDate, opt => opt.MapFrom(x => x.MatchDate.Value.UtcDateTime.ConvertFromUtcToBst())).ReverseMap();

            CreateMap<SwissMatchDetailVm, SwissMatchDetailsViewModel>();
            CreateMap<SwissMatchDto, MatchDetailsDto>();

            CreateMap<GameViewModel, GameVm>().ReverseMap();
            CreateMap<GameViewModel, CreateGameCommand>().ReverseMap();
            CreateMap<GameViewModel, UpdateGameCommand>().ReverseMap();
            CreateMap<TeamGameViewModel, TeamGameVm>().ReverseMap();
            CreateMap<TeamPlayerViewModel, PlayerWeaponDto>().ReverseMap();
            CreateMap<PlayerWeaponViewModel, PlayerWeaponDto>().ReverseMap();
            CreateMap<GameSettingViewModel, GameSettingsDto>().ReverseMap();
        }
    }
}
