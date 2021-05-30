using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Maps.Queries.GetAllMaps
{
    public class GetAllMapsQueryHandler : IRequestHandler<GetAllMapsQuery, List<MapVm>>
    {
        private readonly IAsyncRepository<GameMap> _mapRepository;
        private IMapper _mapper;

        public GetAllMapsQueryHandler(IMapper mapper, IAsyncRepository<GameMap> mapRepository)
        {
            this._mapper = mapper;
            this._mapRepository = mapRepository;
        }

        public async Task<List<MapVm>> Handle(GetAllMapsQuery request, CancellationToken cancellationToken)
        {
            var maps = await this._mapRepository.GetAllAsync();
            var mappedMaps = this._mapper.Map<List<MapVm>>(maps);
            return mappedMaps;
        }
    }
}
