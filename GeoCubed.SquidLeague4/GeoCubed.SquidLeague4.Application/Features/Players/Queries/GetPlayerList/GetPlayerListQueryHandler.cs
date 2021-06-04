using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerList
{
    public class GetPlayerListQueryHandler : IRequestHandler<GetPlayerListQuery, List<PlayerDetailVM>>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public GetPlayerListQueryHandler(IMapper mapper, IPlayerRepository playerRepository)
        {
            this._mapper = mapper;
            this._playerRepository = playerRepository;
        }

        public async Task<List<PlayerDetailVM>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = await this._playerRepository.GetAllPlayersWithTeams();
            var playerDetailList = this._mapper.Map<List<PlayerDetailVM>>(players);
            return playerDetailList;
        }
    }
}
