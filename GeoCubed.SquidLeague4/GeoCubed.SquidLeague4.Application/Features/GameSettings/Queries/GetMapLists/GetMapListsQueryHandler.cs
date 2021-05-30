using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetMapLists
{
    public class GetMapListsQueryHandler : IRequestHandler<GetMapListsQuery, List<MapListVm>>
    {
        private readonly IGameSettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public GetMapListsQueryHandler(IMapper mapper, IGameSettingRepository gameSettingRepository)
        {
            this._mapper = mapper;
            this._settingRepository = gameSettingRepository;
        }

        public async Task<List<MapListVm>> Handle(GetMapListsQuery request, CancellationToken cancellationToken)
        {
            var mapLists = await this._settingRepository.GetMapLists();
            var mappedLists = this._mapper.Map<List<MapListVm>>(mapLists);
            return mappedLists;
        }
    }
}
