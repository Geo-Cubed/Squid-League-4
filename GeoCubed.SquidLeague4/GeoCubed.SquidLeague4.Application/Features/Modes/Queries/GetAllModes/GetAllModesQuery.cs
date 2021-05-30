using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Modes.Queries.GetAllModes
{
    public class GetAllModesQuery : IRequest<List<ModeVm>>
    {
    }
}