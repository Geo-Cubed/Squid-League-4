using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.UpdatePlayer
{
    public class UpdatePlayerCommand : IRequest<UpdatePlayerCommandResponse>
    {
        public int Id { get; set; }

        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string TcRank { get; set; }

        public string RmRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }

        public bool IsActive { get; set; }
    }
}
