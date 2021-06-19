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

namespace GeoCubed.SquidLeague4.Application.Features.Bracket.Commands.CreateKnockoutMatch
{
    public class CreateKnockoutMatchCommandHandler : IRequestHandler<CreateKnockoutMatchCommand, CreateKnockoutMatchCommandResponse>
    {
        private readonly IAsyncRepository<BracketKnockout> _bracketRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ISystemSwitchRepository _switchRepository;
        private readonly IMapper _mapper;

        public CreateKnockoutMatchCommandHandler(
            IMapper mapper, 
            ISystemSwitchRepository systemSwitchRepository, 
            IMatchRepository matchRepository, 
            IAsyncRepository<BracketKnockout> bracketRepository)
        {
            this._mapper = mapper;
            this._switchRepository = systemSwitchRepository;
            this._matchRepository = matchRepository;
            this._bracketRepository = bracketRepository;
        }

        public async Task<CreateKnockoutMatchCommandResponse> Handle(CreateKnockoutMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateKnockoutMatchCommandResponse();

            var validator = new CreateKnockoutMatchCommandValidator(this._matchRepository, this._switchRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
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
                var matchToAdd = this._mapper.Map<BracketKnockout>(request);
                var match = await this._bracketRepository.AddAsync(matchToAdd);
                response.KnockoutMatch = this._mapper.Map<BracketCommandDto>(match);
            }

            return response;
        }
    }
}
