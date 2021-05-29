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

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.UpdateSwitch
{
    public class UpdateSwitchCommandHandler : IRequestHandler<UpdateSwitchCommand, UpdateSwitchCommandResponse>
    {
        private readonly ISystemSwitchRepository _switchRepository;
        private readonly IMapper _mapper;

        public UpdateSwitchCommandHandler(IMapper mapper, ISystemSwitchRepository switchRepository)
        {
            this._mapper = mapper;
            this._switchRepository = switchRepository;
        }

        public async Task<UpdateSwitchCommandResponse> Handle(UpdateSwitchCommand request, CancellationToken cancellationToken)
        {
            request.Name = request.Name.Trim().ToUpper().Replace(" ", "_");
            request.Value = request.Value.Trim();
            var response = new UpdateSwitchCommandResponse();

            var validator = new UpdateSwitchCommandValidator(this._switchRepository);
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
                response.Success = await this._switchRepository.UpdateAsync(sysSwitch);
                if (!response.Success)
                {
                    response.Message = "There was an issue updating the system switch";
                }
            }

            return response;
        }
    }
}
