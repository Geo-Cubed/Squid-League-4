using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetWeaponList
{
    public class GetWeaponListQueryHandler : IRequestHandler<GetWeaponListQuery, List<WeaponVm>>
    {
        private readonly IWeaponRepository _weaponRepository;
        private readonly IMapper _mapper;

        public GetWeaponListQueryHandler(IMapper mapper, IWeaponRepository weaponRepository)
        {
            this._mapper = mapper;
            this._weaponRepository = weaponRepository;
        }

        public async Task<List<WeaponVm>> Handle(GetWeaponListQuery request, CancellationToken cancellationToken)
        {
            var weapons = await this._weaponRepository.GetAllWeaponsAndSubSpecials();
            var mappedWeapons = this._mapper.Map<List<WeaponVm>>(weapons);
            return mappedWeapons;
        }
    }
}
