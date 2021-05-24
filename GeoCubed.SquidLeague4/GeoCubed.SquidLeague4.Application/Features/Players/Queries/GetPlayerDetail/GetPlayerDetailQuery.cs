using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Players.Queries.GetPlayerDetail
{
    public class GetPlayerDetailQuery : IRequest<PlayerDetailVM>
    {
        public int Id { get; set; }
    }
}
