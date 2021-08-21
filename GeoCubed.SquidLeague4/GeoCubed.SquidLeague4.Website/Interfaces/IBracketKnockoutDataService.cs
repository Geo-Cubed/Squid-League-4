using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using GeoCubed.SquidLeague4.Website.ViewModels.KnockoutMatches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IBracketKnockoutDataService
    {
        Task<List<AdminKnockoutMatchViewModel>> GetUpperBracketMatches();

        Task<List<AdminKnockoutMatchViewModel>> GetLowerBracketMatches();

        Task<List<KnockoutInfoViewModel>> GetUpperMatches();

        Task<List<KnockoutInfoViewModel>> GetLowerMatches();

        Task<ApiResponse<int>> CreateKnockoutMatch(AdminKnockoutMatchViewModel knockoutMatch);

        Task<ApiResponse<int>> DeleteKnockoutMatch(int id);
    }
}
