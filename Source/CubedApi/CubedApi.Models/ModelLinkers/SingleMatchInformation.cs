using CubedApi.Models.DatabaseTables;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Models.ModelLinkers
{
    public class SingleMatchInformation
    {
        public SingleMatchInformation()
        {
            this.SetGames = new List<GamePlayed>();
        }

        // match information (overall socre, teams, date, vod, caster)
        public Match MatchInforamtion { get; set; }

        // list of games (each has scores, map, mode, players -> weapon + any other weapon info (could include sub + special))
        public List<GamePlayed> SetGames { get; set; }
    }
}
