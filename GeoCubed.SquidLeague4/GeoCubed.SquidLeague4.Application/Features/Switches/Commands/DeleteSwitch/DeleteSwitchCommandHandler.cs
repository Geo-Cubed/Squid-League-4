using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Commands.DeleteSwitch
{
    public class DeleteSwitchCommandHandler : IRequestHandler<DeleteSwitchCommand, DeleteSwitchCommandResponse>
    {
        private readonly ISystemSwitchRepository _switchRepository;
        private readonly IMapper _mapper;

        public DeleteSwitchCommandHandler(IMapper mapper, ISystemSwitchRepository switchRepository)
        {
            this._mapper = mapper;
            this._switchRepository = switchRepository;
        }

        public async Task<DeleteSwitchCommandResponse> Handle(DeleteSwitchCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteSwitchCommandResponse();

            var validator = new DeleteSwitchCommandValidator(this._switchRepository);
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
                var switchToDelete = await this._switchRepository.GetByIdAsync(request.Id);
                response.Success = await this._switchRepository.DeleteAsync(switchToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue trying to delete the switch";
                }
            }

            return response;
        }
    }
}
