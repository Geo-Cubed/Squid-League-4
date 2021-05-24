using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByMatchId
{
    public class GetGamesByMatchIdQueryHandler : IRequestHandler<GetGamesByMatchIdQuery, List<MatchGameVm>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGamesByMatchIdQueryHandler(IMapper mapper, IGameRepository gameRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
        }

        public async Task<List<MatchGameVm>> Handle(GetGamesByMatchIdQuery request, CancellationToken cancellationToken)
        {
            var games = await this._gameRepository.GetGamesByMatchId(request.MatchId);
            var mappedGames = this._mapper.Map<List<MatchGameVm>>(games);
            return mappedGames;
        }
    }
}
