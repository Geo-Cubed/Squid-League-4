using System.ComponentModel.DataAnnotations;

namespace GeoCubed.SquidLeague4.Application.Models.Authentication
{
    public class RegistrationRequest
    {
        [Required]
        [MinLength(4)]
        public string UserName { get; set; }

        [Required]
        [MinLength(6)]
        public string Password { get; set; }
    }
}
