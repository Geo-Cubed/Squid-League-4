using GeoCubed.SquidLeague4.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IWeaponDataService
    {
        Task<List<BasicWeaponInfo>> GetBasicWeaponInfo();
    }
}
