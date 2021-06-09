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

namespace GeoCubed.SquidLeague4.Application.Features.Swiss.Commands.CreateSwissMatch
{
    public class CreateSwissMatchCommandHandler : IRequestHandler<CreateSwissMatchCommand, CreateSwissMatchCommandResponse>
    {
        private readonly ISwissMatchRepository _swissMatchRepository;
        private readonly IMatchRepository _matchRepository;
        private readonly ISystemSwitchRepository _switchRepository;
        private readonly IMapper _mapper;

        public CreateSwissMatchCommandHandler(
            IMapper mapper, 
            ISwissMatchRepository swissMatchRepository,
            IMatchRepository matchRepository, 
            ISystemSwitchRepository switchRepository)
        {
            this._mapper = mapper;
            this._swissMatchRepository = swissMatchRepository;
            this._matchRepository = matchRepository;
            this._switchRepository = switchRepository;
        }

        public async Task<CreateSwissMatchCommandResponse> Handle(CreateSwissMatchCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateSwissMatchCommandResponse();

            var validator = new CreateSwissMatchCommandValidator(this._matchRepository, this._switchRepository);
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
                var swiss = this._mapper.Map<BracketSwiss>(request);
                swiss = await this._swissMatchRepository.AddAsync(swiss);
                response.SwissMatch = this._mapper.Map<SwissCommandDto>(swiss);
            }

            return response;
        }
    }
}
