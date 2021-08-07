using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Queries.GetAllStatsForAdmin
{
    public record GetAllStatsForAdminQuery() : IRequest<List<AdminStatsVm>>;
}