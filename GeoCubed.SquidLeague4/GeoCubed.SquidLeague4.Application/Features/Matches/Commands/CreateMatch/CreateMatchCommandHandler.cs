using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.CreateMatch
{
    public class CreateMatchCommandHandler : IRequestHandler<CreateMatchCommand, CreateMatchCommandResponse>
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly ICasterRepository _casterRepository;
        private readonly IMapper _mapper;

        public CreateMatchCommandHandler(IMapper mapper, IMatchRepository matchRepository, ITeamRepository teamRepository, ICasterRepository casterRepository)
        {
            this._mapper = mapper;
            this._matchRepository = matchRepository;
            this._teamRepository = teamRepository;
            this._casterRepository = casterRepository;
        }

        public async Task<CreateMatchCommandResponse> Handle(CreateMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateMatchCommandResponse();

            var validator = new CreateMatchCommandValidator(this._teamRepository, this._casterRepository);
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
                var matchToAdd = this._mapper.Map<Match>(request);
                var match = await this._matchRepository.AddAsync(matchToAdd);
                response.Match = this._mapper.Map<MatchCommandDto>(match);
            }

            return response;
        }
    }
}
