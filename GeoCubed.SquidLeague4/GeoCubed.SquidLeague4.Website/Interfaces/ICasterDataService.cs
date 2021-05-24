using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ICasterDataService
    {
        Task<List<CasterDetailViewModel>> GetAllCasters();
        Task<CasterDetailViewModel> GetCasterById(int id);
        Task<ApiResponse<int>> CreateCaster(CasterDetailViewModel casterDetailViewModel);
        Task<ApiResponse<int>> UpdateCaster(CasterDetailViewModel casterDetailViewModel);
        Task<ApiResponse<int>> DeleteCaster(int id);
    }
}
