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

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.CreateSwitch
{
    public class CreateSwitchCommandHandler : IRequestHandler<CreateSwitchCommand, CreateSwitchCommandResponse>
    {
        private readonly IAsyncRepository<SystemSwitch> _switchRepository;
        private readonly IMapper _mapper;

        public CreateSwitchCommandHandler(IMapper mapper, IAsyncRepository<SystemSwitch> switchRepository)
        {
            this._mapper = mapper;
            this._switchRepository = switchRepository;
        }

        public async Task<CreateSwitchCommandResponse> Handle(CreateSwitchCommand request, CancellationToken cancellationToken)
        {
            request.Name = request.Name.Trim().ToUpper().Replace(" ", "_");
            request.Value = request.Value.Trim();
            var response = new CreateSwitchCommandResponse();

            var validator = new CreateSwitchCommandValidator();
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
                var sysSwitch = this._mapper.Map<SystemSwitch>(request);
                sysSwitch = await this._switchRepository.AddAsync(sysSwitch);
                response.Switch = this._mapper.Map<SwitchCommandDto>(sysSwitch);
            }

            return response;
        }
    }
}
