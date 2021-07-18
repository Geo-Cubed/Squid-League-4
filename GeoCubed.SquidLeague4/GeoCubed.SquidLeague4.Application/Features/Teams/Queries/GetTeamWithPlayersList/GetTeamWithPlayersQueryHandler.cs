using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Teams.Queries.GetTeamWithPlayersList
{
    public class GetTeamWithPlayersQueryHandler : IRequestHandler<GetTeamWithPlayersQuery, TeamWithPlayersVm>
    {
        private readonly ITeamRepository _teamRepository;
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMapper _mapper;

        public GetTeamWithPlayersQueryHandler(IMapper mapper, ITeamRepository teamRepository, IWeaponRepository weaponRepository)
        {
            this._teamRepository = teamRepository;
            this._weaponRepository = weaponRepository;
            this._mapper = mapper;
        }

        public async Task<TeamWithPlayersVm> Handle(GetTeamWithPlayersQuery request, CancellationToken cancellationToken)
        {
            var team = await this._teamRepository.GetTeamWithPlayers(request.TeamId);
            var mappedTeams= this._mapper.Map<TeamWithPlayersVm>(team);
            foreach (var player in mappedTeams.Players)
            {
                var weapons = await this._weaponRepository.GetPlayerWeapons(player.Id);
                player.Weapons = this._mapper.Map<List<CommonWeaponDto>>(weapons.Take(3));
            }

            return mappedTeams;
        }
    }
}
