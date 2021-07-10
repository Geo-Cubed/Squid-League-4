using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetBasicWeaponInfo
{
    public record GetBasicWeaponInfoQuery() : IRequest<List<BasicWeaponInfoVm>>;
}