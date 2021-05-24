using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Website.ViewModels
{
    public class CasterDetailViewModel
    {
        public int Id { get; set; }

        public string CasterName { get; set; }

        public string Twitter { get; set; }

        public string YouTube { get; set; }

        public string Twitch { get; set; }

        public string Discord { get; set; }

        public string ProfilePicturePath { get; set; }

        public bool IsActive { get; set; }
    }
}
