using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllSwissWeeks
{
    public record GetAllSwissWeeksQuery() : IRequest<List<int>>;
}