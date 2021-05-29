using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.GameSettings.Queries.GetGameSettingsForAdmin
{
    public class GetGameSettingsForAdminQuery : IRequest<List<GameSettingAdminVm>>
    {
    }
}