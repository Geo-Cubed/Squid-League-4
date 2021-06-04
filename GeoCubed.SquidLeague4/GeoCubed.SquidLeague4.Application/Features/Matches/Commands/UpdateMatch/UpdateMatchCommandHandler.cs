using AutoMapper;
using GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.UpdateMatch
{
    public class UpdateMatchCommandHandler : IRequestHandler<UpdateMatchCommand, UpdateMatchCommandResponse>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICasterRepository _casterRepository;
        private readonly IMapper _mapper;

        public UpdateMatchCommandHandler(IMapper mapper, IMatchRepository matchRepository, ITeamRepository teamRepository, ICasterRepository casterRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
            this._teamRepository = teamRepository;
            this._casterRepository = casterRepository;
        }

        public async Task<UpdateMatchCommandResponse> Handle(UpdateMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateMatchCommandResponse();

            request.CasterProfileId = (request.CasterProfileId <= 0) ? null : request.CasterProfileId;
            request.SecondaryCasterProfileId = (request.SecondaryCasterProfileId <= 0) ? null : request.SecondaryCasterProfileId;

            var validator = new UpdateMatchCommandValidator(this._matchRepository, this._teamRepository, this._casterRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count() > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var error in validation.Errors)
                {
                    response.ValidationErrors.Add(error.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var match = this._mapper.Map<Match>(request);
                response.Success = await this._matchRepository.UpdateAsync(match);
                if (!response.Success)
                {
                    response.Match = this._mapper.Map<MatchCommandDto>(match);
                    response.Message = "There was an issue while trying to update the match";
                }
            }

            return response;
        }
    }
}
