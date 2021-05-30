using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Modes.Queries.GetAllModes
{
    public class GetAllModesQueryHandler : IRequestHandler<GetAllModesQuery, List<ModeVm>>
    {
        private readonly IAsyncRepository<GameMode> _modeRepository;
        private readonly IMapper _mapper;

        public GetAllModesQueryHandler(IMapper mapper, IAsyncRepository<GameMode> modeRepository)
        {
            this._mapper = mapper;
            this._modeRepository = modeRepository;
        }

        public async Task<List<ModeVm>> Handle(GetAllModesQuery request, CancellationToken cancellationToken)
        {
            var modes = await this._modeRepository.GetAllAsync();
            var mappedModes = this._mapper.Map<List<ModeVm>>(modes);
            return mappedModes;
        }
    }
}
