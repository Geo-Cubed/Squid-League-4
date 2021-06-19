using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Queries.GetAllUpperBracket
{
    public class GetAllUpperBracketQueryHandler : IRequestHandler<GetAllUpperBracketQuery, List<UpperBracketVm>>
    {
        private readonly IBracketKnockoutRepository _bracketRepository;
        private readonly IMapper _mapper;

        public GetAllUpperBracketQueryHandler(IMapper mapper, IBracketKnockoutRepository bracketRepository)
        {
            this._mapper = mapper;
            this._bracketRepository = bracketRepository;
        }

        public async Task<List<UpperBracketVm>> Handle(GetAllUpperBracketQuery request, CancellationToken cancellationToken)
        {
            var upper = await this._bracketRepository.GetUpperBracket();
            var mappedUpper = this._mapper.Map<List<UpperBracketVm>>(upper);
            return mappedUpper;
        }
    }
}
