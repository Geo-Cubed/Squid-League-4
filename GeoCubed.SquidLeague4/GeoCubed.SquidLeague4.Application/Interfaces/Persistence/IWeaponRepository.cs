using GeoCubed.SquidLeague4.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Interfaces.Persistence
{
    public interface IWeaponRepository : IAsyncRepository<Weapon>
    {
        Task<IReadOnlyList<Weapon>> GetAllWeaponsAndSubSpecials();

        Task<IReadOnlyList<Weapon>> GetPlayerWeapons(int playerId);
    }
}
