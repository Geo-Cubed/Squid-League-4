using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetGamesByTeamId
{
    public class GetGamesByTeamIdQueryHandler : IRequestHandler<GetGamesByTeamIdQuery, List<TeamGameVm>>
    {
        private readonly IGameRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetGamesByTeamIdQueryHandler(IMapper mapper, IGameRepository gameRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
        }

        public async Task<List<TeamGameVm>> Handle(GetGamesByTeamIdQuery request, CancellationToken cancellationToken)
        {
            var games = await this._gameRepository.GetGamesByTeamId(request.TeamId);
            var mappedGames = this._mapper.Map<List<TeamGameVm>>(games);
            return mappedGames;
        }
    }
}
