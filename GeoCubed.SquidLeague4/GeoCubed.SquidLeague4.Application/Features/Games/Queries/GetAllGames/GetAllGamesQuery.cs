using MediatR;
using System.Collections.Generic;

namespace GeoCubed.SquidLeague4.Application.Features.Games.Queries.GetAllGames
{
    public class GetAllGamesQuery : IRequest<List<GameVm>>
    {
    }
}