using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Results.Queries.GetFullSetInfo
{
    public class GetFullSetInfoQueryHandler : IRequestHandler<GetFullSetInfoQuery, List<FullSetInfo>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetFullSetInfoQueryHandler(IGameRepository gameRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
        }

        public async Task<List<FullSetInfo>> Handle(GetFullSetInfoQuery request, CancellationToken cancellationToken)
        {
            var games = await this._gameRepository.GetFullSetInfo(request.MatchId);
            var mappedGames = this._mapper.Map<List<FullSetInfo>>(games);
            return mappedGames;
        }
    }
}
