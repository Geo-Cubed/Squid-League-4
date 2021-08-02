﻿using MediatR;

namespace GeoCubed.SquidLeague4.Application.Features.Stats.Commands.CreateStats
{
    public class CreateStatsCommand : IRequest<CreateStatsCommandResponse>
    {
        public string Alias { get; set; }

        public string Sql { get; set; }
    }
}