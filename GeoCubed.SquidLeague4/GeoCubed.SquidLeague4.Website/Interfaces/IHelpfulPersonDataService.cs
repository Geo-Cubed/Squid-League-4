using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IHelpfulPersonDataService
    {
        Task<List<HelpfulPersonDetailViewModel>> GetAllHelpfulPersons();
        Task<ApiResponse<int>> CreateHelpfulPerson(HelpfulPersonDetailViewModel helpfulPersonDetailViewModel);
        Task<ApiResponse<int>> UpdateHelpfulPerson(HelpfulPersonDetailViewModel helpfulPersonDetailViewModel);
        Task<ApiResponse<int>> DeleteHelpfulPerson(int id);
    }
}
