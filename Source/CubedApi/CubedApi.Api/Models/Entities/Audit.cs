using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models.Entities
{
    public partial class Audit
    {
        public int Id { get; set; }
        public string TableName { get; set; }
        public string ChangeType { get; set; }
        public string OldRow { get; set; }
        public string NewRow { get; set; }
        public string Username { get; set; }
    }
}
