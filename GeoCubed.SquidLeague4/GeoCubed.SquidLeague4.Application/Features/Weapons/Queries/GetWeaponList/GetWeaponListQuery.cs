using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList
{
    public class GetWeaponListQuery : IRequest<List<WeaponVm>>
    {
    }
}