using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Matches.Commands.DeleteMatch
{
    public class DeleteMatchCommand : IRequest<DeleteMatchCommandResponse>
    {
        public int Id { get; set; }
    }
}