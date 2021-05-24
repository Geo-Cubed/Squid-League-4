using AutoMapper;
using GeoCubed.SquidLeague4.Application.Exceptions;
using GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchList;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Queries.GetMatchById
{
    public class GetMatchByIdQueryHandler : IRequestHandler<GetMatchByIdQuery, MatchDetailVm>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public GetMatchByIdQueryHandler(IMapper mapper, IMatchRepository matchRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
        }

        public async Task<MatchDetailVm> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
        {
            if (request.Id <= 0)
            {
                throw new BadRequestException("Invalid match Id");
            }

            var match = await this._matchRepository.GetMatchById(request.Id);
            if (match == null)
            {
                throw new NotFoundException("Match", request.Id);
            }

            var mappedMatch = this._mapper.Map<MatchDetailVm>(match);
            return mappedMatch;
        }
    }
}
