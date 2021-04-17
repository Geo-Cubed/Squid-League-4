using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public int Username { get; set; }
        public string PasswordHash { get; set; }
        public bool? IsAdmin { get; set; }
    }
}
