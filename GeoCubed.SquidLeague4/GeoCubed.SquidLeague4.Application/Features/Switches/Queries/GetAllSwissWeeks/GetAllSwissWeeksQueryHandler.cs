using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwissWeeks
{
    public class GetAllSwissWeeksQueryHandler : IRequestHandler<GetAllSwissWeeksQuery, List<int>>
    {
        private readonly ISystemSwitchRepository _switchRepository;

        public GetAllSwissWeeksQueryHandler(ISystemSwitchRepository systemSwitchRepository)
        {
            this._switchRepository = systemSwitchRepository;
        }

        public async Task<List<int>> Handle(GetAllSwissWeeksQuery request, CancellationToken cancellationToken)
        {
            var weeks = await this._switchRepository.GetSwissWeeks();
            return weeks.ToList();
        }
    }
}
