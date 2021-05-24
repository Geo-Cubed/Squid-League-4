using GeoCubed.SquidLeague4.Website.ViewModels.SwissMatches;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface ISwissDataService
    {
        Task<List<SwissMatchDetailsViewModel>> GetAllSwissMatches();
    }
}
