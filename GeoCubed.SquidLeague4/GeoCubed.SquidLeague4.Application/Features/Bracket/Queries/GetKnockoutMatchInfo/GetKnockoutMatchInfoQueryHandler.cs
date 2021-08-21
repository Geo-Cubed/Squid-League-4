using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetKnockoutMatchInfo
{
    public class GetKnockoutMatchInfoQueryHandler : IRequestHandler<GetKnockoutMatchInfoQuery, List<KnockoutMatchInfo>>
    {
        private readonly IBracketKnockoutRepository _knockoutRepository;
        private readonly IMapper _mapper;

        public GetKnockoutMatchInfoQueryHandler(IBracketKnockoutRepository knockoutRepository, IMapper mapper)
        {
            this._knockoutRepository = knockoutRepository;
            this._mapper = mapper;
        }

        public async Task<List<KnockoutMatchInfo>> Handle(GetKnockoutMatchInfoQuery request, CancellationToken cancellationToken)
        {
            var matchInfo = await this._knockoutRepository.GetKnockoutInformation(request.isUpper);
            var mappedMatches = this._mapper.Map<List<KnockoutMatchInfo>>(matchInfo);
            return mappedMatches;
        }
    }
}
