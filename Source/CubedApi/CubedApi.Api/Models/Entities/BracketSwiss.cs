using System;
using System.Collections.Generic;

#nullable disable

namespace CubedApi.Api.Models.Entities
{
    public partial class BracketSwiss
    {
        public int Id { get; set; }
        public int MatchId { get; set; }
        public int MatchWeek { get; set; }

        public virtual Match Match { get; set; }
    }
}
