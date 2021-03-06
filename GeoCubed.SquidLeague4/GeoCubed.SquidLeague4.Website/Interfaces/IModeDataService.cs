﻿using GeoCubed.SquidLeague4.Website.ViewModels.Admin;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.Interfaces
{
    public interface IModeDataService
    {
        Task<List<AdminModeViewModel>> GetAllModesForAdmin();
    }
}
