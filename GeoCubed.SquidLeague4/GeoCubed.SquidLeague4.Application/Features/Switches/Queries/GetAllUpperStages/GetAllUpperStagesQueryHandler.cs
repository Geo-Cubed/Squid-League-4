using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllUpperStages
{
    public class GetAllUpperStagesQueryHandler : IRequestHandler<GetAllUpperStagesQuery, List<string>>
    {
        private readonly ISystemSwitchRepository _switchRepository;

        public GetAllUpperStagesQueryHandler(ISystemSwitchRepository systemSwitchRepository)
        {
            this._switchRepository = systemSwitchRepository;
        }

        public async Task<List<string>> Handle(GetAllUpperStagesQuery request, CancellationToken cancellationToken)
        {
            var stages = await this._switchRepository.GetUpperStages();
            return stages.ToList();
        }
    }
}
