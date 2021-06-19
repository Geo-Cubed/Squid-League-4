using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IBracketKnockoutDataService
    {
        Task<List<AdminKnockoutMatchViewModel>> GetUpperBracketMatches();

        Task<List<AdminKnockoutMatchViewModel>> GetLowerBracketMatches();

        Task<ApiResponse<int>> CreateKnockoutMatch(AdminKnockoutMatchViewModel knockoutMatch);

        Task<ApiResponse<int>> DeleteKnockoutMatch(int id);
    }
}
