using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Commands.CreatePlayer
{
    public class CreatePlayerCommand : IRequest<CreatePlayerCommandResponse>
    {
        public string InGameName { get; set; }

        public string SzRank { get; set; }

        public string RmRank { get; set; }

        public string TcRank { get; set; }

        public string CbRank { get; set; }

        public int? TeamId { get; set; }

        public bool IsActive { get; set; }

        public override string ToString()
        {
            return $"Player: {this.InGameName}; SZ: {this.SzRank}; TC: {this.TcRank}; RM: {this.RmRank}; CB: {this.CbRank};";
        }
    }
}
