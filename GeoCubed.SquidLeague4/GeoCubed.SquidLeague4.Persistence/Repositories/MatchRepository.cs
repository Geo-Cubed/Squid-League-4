﻿using GeoCubed.SquidLeague4.Application.Interfaces.Persistence;
using GeoCubed.SquidLeague4.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GeoCubed.SquidLeague4.Persistence.Repositories
{
    public class MatchRepository : BaseRepository<Match>, IMatchRepository
    {
        public MatchRepository(SquidLeagueDbContext context) : base (context)
        {
        }

        public Task<bool> DoesMatchExist(int id)
        {
            var match = this._dbContext.Matches.AsNoTracking().FirstOrDefault(m => m.Id == id);
            return Task.FromResult(match != null);
        }

        public Task<IReadOnlyList<Match>> GetAllMatchesAsync()
        {
            var matches = this._dbContext.Matches
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .Include(m => m.CasterProfile)
                .Include(m => m.SecondaryCasterProfile).ToList();

            return Task.FromResult((IReadOnlyList<Match>)matches);
        }

        public Task<Match> GetMatchById(int id)
        {
            var match = this._dbContext.Matches
                .Where(m => m.Id == id)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam)
                .FirstOrDefault();

            return Task.FromResult(match);
        }

        public Task<string> GetStage(int id)
        {
            var stage = this._dbContext.BracketSwisses
                .Where(x => x.MatchId == id)
                .Select(x => x.MatchWeek)
                .FirstOrDefault();

            if (stage >= 1)
            {
                return Task.FromResult(string.Format("Week: {0}", stage.ToString()));
            }

            var knockoutStage = this._dbContext.BracketKnockouts
                .Where(x => x.MatchId == id)
                .Select(x => x.Stage)
                .FirstOrDefault();

            if (!string.IsNullOrEmpty(knockoutStage))
            {
                return Task.FromResult(knockoutStage);
            }

            return Task.FromResult(string.Empty);
        }

        public Task<IReadOnlyList<Match>> GetTeamPlayedMatches(int teamId)
        {
            var matches = this._dbContext.Matches
                .Where(m => (m.HomeTeamId == teamId || m.AwayTeamId == teamId) && m.Winner != "none")
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam).ToList();

            return Task.FromResult((IReadOnlyList<Match>)matches);
        }

        public Task<IReadOnlyList<Match>> GetUpcommingMatchesAsync()
        {
            var endDate = DateTime.UtcNow.AddDays(7).Date;
            var matches = this._dbContext.Matches
                .Where(m =>
                    m.MatchDate.HasValue
                    && m.MatchDate.Value > DateTime.UtcNow.Date
                    && m.MatchDate.Value < endDate)
                .Include(m => m.HomeTeam)
                .Include(m => m.AwayTeam).ToList();

            return Task.FromResult((IReadOnlyList<Match>)matches);
        }
    }
}
