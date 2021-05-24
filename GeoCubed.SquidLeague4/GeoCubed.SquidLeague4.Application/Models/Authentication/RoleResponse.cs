using GeoCubed.SquidLeague4.Application.Models.Authentication.Enum;

namespace GeoCubed.SquidLeague4.Application.Models.Authentication
{
    public class RoleResponse
    {
        public string Username { get; set; }

        public string Role { get; set; }

        public RoleStatus Status { get; set; }

        public string Message { get; set; }
    }
}
