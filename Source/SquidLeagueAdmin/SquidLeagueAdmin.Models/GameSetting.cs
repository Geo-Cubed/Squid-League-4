using SquidLeagueAdmin.Models.Enums;
using SquidLeagueAdmin.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Models
{
    public class GameSetting
    {
        public int Id;

        public int MapId;

        public GameModes Mode;

        public string BracketStage;

        public int SortOrder;

        public override string ToString()
        {
            if (this.Id <= 0)
            {
                return "New Setting";
            }
            else
            {
                return $"Game: {this.SortOrder} Stage: {this.BracketStage} Mode: {Mode.GetDescription()}";
            }
        }
    }
}
