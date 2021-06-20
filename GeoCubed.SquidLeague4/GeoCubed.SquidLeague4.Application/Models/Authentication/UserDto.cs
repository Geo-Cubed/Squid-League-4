using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Models.Authentication
{
    public class UserDto
    {
        public UserDto()
        {
            this.Roles = new List<string>();
        }

        public string UserName { get; set; }
        
        public List<string> Roles { get; set; }
    }
}
