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

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.UpdateGameSetting
{
    public class UpdateGameSettingCommandHandler : IRequestHandler<UpdateGameSettingCommand, UpdateGameSettingCommandResponse>
    {
        private readonly IGameSettingRepository _settingRepository;
        private readonly IMapRepository _mapRepository;
        private readonly IModeRepository _modeRepository;
        private readonly IMapper _mapper;

        public UpdateGameSettingCommandHandler(IMapper mapper, IGameSettingRepository gameSettingRepository, IMapRepository mapRepository, IModeRepository modeRepository)
        {
            this._mapper = mapper;
            this._mapRepository = mapRepository;
            this._modeRepository = modeRepository;
            this._settingRepository = gameSettingRepository;
        }

        public async Task<UpdateGameSettingCommandResponse> Handle(UpdateGameSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new UpdateGameSettingCommandResponse();

            var validator = new UpdateGameSettingCommandValidator(this._settingRepository, this._modeRepository, this._mapRepository);
            var validation = await validator.ValidateAsync(request);
            if (validation.Errors.Count > 0)
            {
                response.Success = false;
                response.ValidationErrors = new List<string>();
                foreach (var errors in validation.Errors)
                {
                    response.ValidationErrors.Add(errors.ErrorMessage);
                }
            }

            if (response.Success)
            {
                var setting = this._mapper.Map<GameSetting>(request);
                response.Success = await this._settingRepository.UpdateAsync(setting);
                if (!response.Success)
                {
                    response.Message = "There was an issue trying to update the game setting";
                }
            }

            return response;
        }
    }
}
