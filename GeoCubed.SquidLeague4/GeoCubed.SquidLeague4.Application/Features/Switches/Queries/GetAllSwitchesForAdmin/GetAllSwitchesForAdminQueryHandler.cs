using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwitchesForAdmin
{
    public class GetAllSwitchesForAdminQueryHandler : IRequestHandler<GetAllSwitchesForAdminQuery, List<SystemSwitchAdminVm>>
    {
        private readonly IAsyncRepository<SystemSwitch> _switchRepository;
        private readonly IMapper _mapper;

        public GetAllSwitchesForAdminQueryHandler(IMapper mapper, IAsyncRepository<SystemSwitch> switchRepository)
        {
            this._mapper = mapper;
            this._switchRepository = switchRepository;
        }

        public async Task<List<SystemSwitchAdminVm>> Handle(GetAllSwitchesForAdminQuery request, CancellationToken cancellationToken)
        {
            var switches = await this._switchRepository.GetAllAsync();
            var mappedSwitches = this._mapper.Map<List<SystemSwitchAdminVm>>(switches);
            return mappedSwitches;
        }
    }
}
