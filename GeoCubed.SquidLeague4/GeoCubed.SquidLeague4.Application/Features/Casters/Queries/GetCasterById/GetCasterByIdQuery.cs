using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Casters.Queries.GetCasterById
{
    public class GetCasterByIdQuery : IRequest<CasterVm>
    {
        public int Id { get; set; }
    }
}
