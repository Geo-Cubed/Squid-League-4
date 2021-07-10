using AutoMapper;
using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Weapons.Queries.GetBasicWeaponInfo
{
    public class GetBasicWeaponInfoQueryHandler : IRequestHandler<GetBasicWeaponInfoQuery, List<BasicWeaponInfoVm>>
    {
        private readonly IAsyncRepository<Weapon> _weaponRepository;
        private readonly IMapper _mapper;

        public GetBasicWeaponInfoQueryHandler(IMapper mapper, IAsyncRepository<Weapon> weaponRepository)
        {
            this._mapper = mapper;
            this._weaponRepository = weaponRepository;
        }

        public async Task<List<BasicWeaponInfoVm>> Handle(GetBasicWeaponInfoQuery request, CancellationToken cancellationToken)
        {
            var weapons = await this._weaponRepository.GetAllAsync();
            var mappedWeapons = this._mapper.Map<List<BasicWeaponInfoVm>>(weapons);
            return mappedWeapons;
        }
    }
}
