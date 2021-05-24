using AutoMapper;
using GeoCubed.SquidLeague4.Application.Common.Helpers;
using GeoCubed.SquidLeague4.Application.Exceptions;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQueryHandler : IRequestHandler<GetPlayerDetailQuery, PlayerDetailVM>
    {
        private readonly IPlayerRepository _playerRepository;
        private readonly IMapper _mapper;

        public GetPlayerDetailQueryHandler(IMapper mapper, IPlayerRepository playerRepository)
        {
            this._mapper = mapper ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(mapper.GetType(), this.GetType()));
            this._playerRepository = playerRepository ?? 
                throw new ArgumentException(ErrorMessageHeleper.GetNullArguementMessage(playerRepository.GetType(), this.GetType()));
        }

        public async Task<PlayerDetailVM> Handle(GetPlayerDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Invalid request id.");
            }

            var player = await this._playerRepository.GetByIdWithTeam(request.Id);
            if (player == null)
            {
                throw new NotFoundException("Player", request.Id);
            }

            var playerDetailVm = this._mapper.Map<PlayerDetailVM>(player);
            return playerDetailVm;
        }
    }
}
