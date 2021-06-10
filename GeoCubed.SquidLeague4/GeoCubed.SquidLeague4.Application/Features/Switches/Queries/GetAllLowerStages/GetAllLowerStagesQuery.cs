using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllLowerStages
{
    public record GetAllLowerStagesQuery() : IRequest<List<string>>;
}