using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ISwissDataService
    {
        Task<List<SwissMatchDetailsViewModel>> GetAllSwissMatches();

        Task<List<AdminBracketSwissViewModel>> GetSwissMatchesForAdmin();

        Task<ApiResponse<int>> CreateSwissMatch(AdminBracketSwissViewModel adminSwissMatchViewModel);

        Task<ApiResponse<int>> DeleteSwissMatch(int id);
    }
}
