using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Models.Authentication
{
    public class DeleteResponse
    {
        public bool Success { get; set; }

        public List<string> Message { get; set; }
    }
}
