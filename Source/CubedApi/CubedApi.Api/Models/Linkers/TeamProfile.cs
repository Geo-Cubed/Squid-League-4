using CubedApi.Api.Models.DTOs;
using System.Collections.Generic;

namespace CubedApi.Api.Models.Linkers
{
    public class TeamProfile
    {
        public TeamProfile()
        {
            this.players = new List<PlayerDto>();
        }

        public TeamDto team { get; set; }

        public List<PlayerDto> players { get; set; }
    }
}
