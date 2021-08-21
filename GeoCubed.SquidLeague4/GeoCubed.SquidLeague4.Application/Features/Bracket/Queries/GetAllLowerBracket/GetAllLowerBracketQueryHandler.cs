using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllLowerBracket
{
    public class GetAllLowerBracketQueryHandler : IRequestHandler<GetAllLowerBracketQuery, List<LowerBracketVm>>
    {
        private readonly IBracketKnockoutRepository _bracketRepository;
        private readonly IMapper _mapper;

        public GetAllLowerBracketQueryHandler(IMapper mapper, IBracketKnockoutRepository bracketRepository)
        {
            this._mapper = mapper;
            this._bracketRepository = bracketRepository;
        }

        public async Task<List<LowerBracketVm>> Handle(GetAllLowerBracketQuery request, CancellationToken cancellationToken)
        {
            var lower = await this._bracketRepository.GetLowerBracket();
            var mappedLower = this._mapper.Map<List<LowerBracketVm>>(lower);
            return mappedLower;
        }
    }
}
