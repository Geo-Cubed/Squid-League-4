using GeoCubed.SquidLeague4.Website.Services.Base;
using GeoCubed.SquidLeague4.Website.ViewModels;
using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IHelpfulPersonDataService
    {
        Task<List<HelpfulPersonDetailViewModel>> GetAllHelpfulPersons();
        Task<List<AdminHelpfulPeopleViewModel>> GetHelpfulPeopleForAdmin();
        Task<ApiResponse<int>> CreateHelpfulPerson(AdminHelpfulPeopleViewModel helpfulPersonDetailViewModel);
        Task<ApiResponse<int>> UpdateHelpfulPerson(AdminHelpfulPeopleViewModel helpfulPersonDetailViewModel);
        Task<ApiResponse<int>> DeleteHelpfulPerson(int id);
    }
}
