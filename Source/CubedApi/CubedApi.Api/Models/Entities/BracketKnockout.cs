using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models.Entities
{
    public partial class BracketKnockout
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public string Stage { get; set; }

        public virtual Match Match { get; set; }
    }
}
