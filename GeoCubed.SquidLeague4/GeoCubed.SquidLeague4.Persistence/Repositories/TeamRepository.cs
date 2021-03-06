﻿using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class TeamRepository : BaseRepository<Team>, ITeamRepository
    {
        public TeamRepository(SquidLeagueDbContext context) : base(context)
        {
        }

        public Task<bool> DoesTeamExist(int id)
        {
            var team = this._dbContext.Teams.AsNoTracking().Any(t => t.Id == id);
            return Task.FromResult(team);
        }

        public Task<Team> GetTeamWithPlayers(int teamId)
        {
            var teams = this._dbContext.Teams
                .Where(t => (bool)t.IsActive && t.Id == teamId)
                .Include(t => t.Players)
                .Include(t => t.MatchAwayTeams)
                .Include(t => t.MatchHomeTeams)
                .FirstOrDefault();
            return Task.FromResult(teams);
        }
    }
}
