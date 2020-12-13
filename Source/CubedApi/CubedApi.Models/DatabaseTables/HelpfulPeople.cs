using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Models.DatabaseTables
{
    public class HelpfulPeople
    {
        public int? Id { get; set; }

        public string UserName { get; set; }

        public string Description { get; set; }

        public string PicturePath { get; set; }

        public string Twitter { get; set; }
    }
}
