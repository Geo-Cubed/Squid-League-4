using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.DeleteGameSetting
{
    public class DeleteGameSettingCommandHandler : IRequestHandler<DeleteGameSettingCommand, DeleteGameSettingCommandResponse>
    {
        private readonly IGameSettingRepository _settingRepository;
        private readonly IMapper _mapper;

        public DeleteGameSettingCommandHandler(IMapper mapper, IGameSettingRepository gameSettingRepository)
        {
            this._mapper = mapper;
            this._settingRepository = gameSettingRepository;
        }

        public async Task<DeleteGameSettingCommandResponse> Handle(DeleteGameSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new DeleteGameSettingCommandResponse();

            var validator = new DeleteGameSettingCommandValidator(this._settingRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
            {
                response.Success = false;
                response.Message = "Game setting does not exist";
            }

            if (response.Success)
            {
                var settingToDelete = await this._settingRepository.GetByIdAsync(request.Id);
                response.Success = await this._settingRepository.DeleteAsync(settingToDelete);
                if (!response.Success)
                {
                    response.Message = "There was an issue deleting the game setting";
                }
            }

            return response;
        }
    }
}
