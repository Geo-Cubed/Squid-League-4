using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames
{
    public class GetAllGamesQueryHandler : IRequestHandler<GetAllGamesQuery, List<GameVm>>
    {
        private readonly IAsyncRepository<Game> _gameRepository;
        private readonly IMapper _mapper;

        public GetAllGamesQueryHandler(IMapper mapper, IAsyncRepository<Game> gameRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameRepository;
        }

        public async Task<List<GameVm>> Handle(GetAllGamesQuery request, CancellationToken cancellationToken)
        {
            var allGames = await this._gameRepository.GetAllAsync();
            var mappedGames = this._mapper.Map<List<GameVm>>(allGames);
            return mappedGames;
        }
    }
}
