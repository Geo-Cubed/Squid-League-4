using CubedApi.Api.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CubedApi.Api.Models.Linkers
{
    public class MatchProfile
    {
        public MatchProfile()
        {
            this.games = new List<GameDto>();
        }

        public MatchDto match { get; set; }

        public List<GameDto> games { get; set; }
    }
}
