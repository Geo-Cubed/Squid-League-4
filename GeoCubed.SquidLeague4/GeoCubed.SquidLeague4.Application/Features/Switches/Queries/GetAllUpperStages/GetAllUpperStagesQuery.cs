using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Switches.Queries.GetAllUpperStages
{
    public record GetAllUpperStagesQuery() : IRequest<List<string>>;
}