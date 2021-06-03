using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.Caster;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ICasterDataService
    {
        Task<List<CasterDetailViewModel>> GetAllCasters();
        Task<List<AdminCasterViewModel>> GetAllCastersForAdmin();
        Task<CasterDetailViewModel> GetCasterById(int id);
        Task<ApiResponse<int>> CreateCaster(AdminCasterViewModel casterDetailViewModel);
        Task<ApiResponse<int>> UpdateCaster(AdminCasterViewModel casterDetailViewModel);
        Task<ApiResponse<int>> DeleteCaster(int id);
    }
}
