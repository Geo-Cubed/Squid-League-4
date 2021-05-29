using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetGameSettingsForAdmin
{
    public class GetGameSettingsForAdminQueryHandler : IRequestHandler<GetGameSettingsForAdminQuery, List<GameSettingAdminVm>>
    {
        private readonly IAsyncRepository<GameSetting> _gameSettingRepository;
        private readonly IMapper _mapper;

        public GetGameSettingsForAdminQueryHandler(IMapper mapper, IAsyncRepository<GameSetting> gameSettingRepository)
        {
            this._mapper = mapper;
            this._gameSettingRepository = gameSettingRepository;
        }

        public async Task<List<GameSettingAdminVm>> Handle(GetGameSettingsForAdminQuery request, CancellationToken cancellationToken)
        {
            var allSettings = await this._gameSettingRepository.GetAllAsync();
            var mappedSettings = this._mapper.Map<List<GameSettingAdminVm>>(allSettings);
            return mappedSettings;
        }
    }
}
