using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class GameSetting
    {
        public int Id;

        public int MapId;

        public int ModeId;

        public string BracketStage;

        public int SortOrder;

        public override string ToString()
        {
            return $"Stage: {this.BracketStage} Game: {this.SortOrder}";
        }
    }
}
