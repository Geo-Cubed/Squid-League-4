using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class GetTeamWithPlayersListQueryHandler : IRequestHandler<GetTeamWithPlayersListQuery, List<TeamWithPlayersVm>>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMapper _mapper;

        public GetTeamWithPlayersListQueryHandler(IMapper mapper, ITeamRepository teamRepository, IWeaponRepository weaponRepository)
        {
            this._teamRepository = teamRepository;
            this._weaponRepository = weaponRepository;
            this._mapper = mapper;
        }

        public async Task<List<TeamWithPlayersVm>> Handle(GetTeamWithPlayersListQuery request, CancellationToken cancellationToken)
        {
            var teams = await this._teamRepository.GetAllTeamsWithPlayers();
            var mappedTeams = this._mapper.Map<List<TeamWithPlayersVm>>(teams);
            foreach (var team in mappedTeams)
            {
                foreach (var player in team.Players)
                {
                    var weapons = await this._weaponRepository.GetPlayerWeapons(player.Id);
                    player.Weapons = this._mapper.Map<List<CommonWeaponDto>>(weapons.Take(3));
                }
            }

            return mappedTeams;
        }
    }
}
