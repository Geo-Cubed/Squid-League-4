using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapsByMatchId
{
    public class GetMapsByMatchIdQueryHandler : IRequestHandler<GetMapsByMatchIdQuery, List<MatchMapListVm>>
    {
        private readonly IGameSettingRepository _gameRepository;
        private readonly IMapper _mapper;

        public GetMapsByMatchIdQueryHandler(IMapper mapper, IGameSettingRepository gameSettingRepository)
        {
            this._mapper = mapper;
            this._gameRepository = gameSettingRepository;
        }

        public async Task<List<MatchMapListVm>> Handle(GetMapsByMatchIdQuery request, CancellationToken cancellationToken)
        {
            var maps = await this._gameRepository.GetMapListByMatchId(request.MatchId);
            var mappedMaps = this._mapper.Map<List<MatchMapListVm>>(maps);
            return mappedMaps;
        }
    }
}
