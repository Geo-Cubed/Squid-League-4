using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
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
            this._mapper = mapper ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._playerRepository = playerRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(playerRepository.GetType(), this.GetType()));
        }

        public async Task<List<PlayerDetailVM>> Handle(GetPlayerListQuery request, CancellationToken cancellationToken)
        {
            var players = await this._playerRepository.GetAllPlayersWithTeams();
            var playerDetailList = this._mapper.Map<List<PlayerDetailVM>>(players);
            return playerDetailList;
        }
    }
}
