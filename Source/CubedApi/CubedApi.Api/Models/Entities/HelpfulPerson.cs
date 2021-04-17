using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models.Entities
{
    public partial class HelpfulPerson
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Description { get; set; }
        public string ProfilePictureLink { get; set; }
        public string TwitterLink { get; set; }
    }
}
