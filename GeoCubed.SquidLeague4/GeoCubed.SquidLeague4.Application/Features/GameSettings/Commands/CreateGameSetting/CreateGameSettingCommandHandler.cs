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

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Commands.CreateGameSetting
{
    public class CreateGameSettingCommandHandler : IRequestHandler<CreateGameSettingCommand, CreateGameSettingCommandResponse>
    {
        private readonly IAsyncRepository<GameSetting> _setttingRepository;
        private readonly IModeRepository _modeRepository;
        private readonly IMapRepository _mapRepository;
        private readonly IMapper _mapper;

        public CreateGameSettingCommandHandler(IMapper mapper, IAsyncRepository<GameSetting> settingRepository, IModeRepository modeRepository, IMapRepository mapRepository)
        {
            this._mapper = mapper;
            this._setttingRepository = settingRepository;
            this._modeRepository = modeRepository;
            this._mapRepository = mapRepository;
        }

        public async Task<CreateGameSettingCommandResponse> Handle(CreateGameSettingCommand request, CancellationToken cancellationToken)
        {
            var response = new CreateGameSettingCommandResponse();

            var validator = new CreateGameSettingCommandValidator(this._mapRepository, this._modeRepository);
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
                var settingToAdd = this._mapper.Map<GameSetting>(request);
                var setting = await this._setttingRepository.AddAsync(settingToAdd);
                response.GameSetting = this._mapper.Map<GameSettingCommandDto>(setting);
            }

            return response;
        }
    }
}
