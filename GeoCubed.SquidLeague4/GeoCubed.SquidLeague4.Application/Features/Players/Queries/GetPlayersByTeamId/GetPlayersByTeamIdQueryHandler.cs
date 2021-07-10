using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayersByTeamId
{
    public class GetPlayersByTeamIdQueryHandler : IRequestHandler<GetPlayersByTeamIdQuery, List<MinimumPlayerInfoVm>>
    {
        private IPlayerRepository _playerRepository;
        private IMapper _mapper;

        public GetPlayersByTeamIdQueryHandler(IMapper mapper, IPlayerRepository playerRepository)
        {
            this._mapper = mapper;
            this._playerRepository = playerRepository;
        }

        public async Task<List<MinimumPlayerInfoVm>> Handle(GetPlayersByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var players = await this._playerRepository.GetAllPlayersByTeamId(request.TeamId);
            var mappedPlayers = this._mapper.Map<List<MinimumPlayerInfoVm>>(players);
            return mappedPlayers;
        }
    }
}
