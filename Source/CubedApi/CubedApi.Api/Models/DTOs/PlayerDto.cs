using System.Collections.Generic;

namespace CubedApi.Api.Models.DTOs
{
    public class PlayerDto
    {
        public int Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }
    }
}
