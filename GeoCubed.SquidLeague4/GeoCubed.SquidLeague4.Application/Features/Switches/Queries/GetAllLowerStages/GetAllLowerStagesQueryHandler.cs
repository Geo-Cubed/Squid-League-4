using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllLowerStages
{
    public class GetAllLowerStagesQueryHandler : IRequestHandler<GetAllLowerStagesQuery, List<string>>
    {
        private readonly ISystemSwitchRepository _switchRepository;

        public GetAllLowerStagesQueryHandler(ISystemSwitchRepository systemSwitchRepository)
        {
            this._switchRepository = systemSwitchRepository;
        }

        public async Task<List<string>> Handle(GetAllLowerStagesQuery request, CancellationToken cancellationToken)
        {
            var stages = await this._switchRepository.GetLowerStages();
            return stages.ToList();
        }
    }
}
