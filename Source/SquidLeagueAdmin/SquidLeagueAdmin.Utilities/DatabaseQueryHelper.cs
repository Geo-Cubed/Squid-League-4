using SquidLeagueAdmin.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace SquidLeagueAdmin.Utilities
{
    public static class DatabaseQueryHelper
    {
        public static string Prefix = "admin";

        // Table names.
        public static string CasterTable = "caster";
        public static string GameSettingTable = "game_setting";
        public static string HelpfulPeopleTable = "helpful_people";
        public static string MapTable = "game_map";
        public static string MatchTable = "match";
        public static string PlayerTable = "player";
        public static string SpecialTable = "weapon_special";
        public static string SubTable = "weapon_sub";
        public static string SystemSwitchTable = "system_switch";
        public static string TeamTable = "team";
        public static string WeaponTable = "weapon";

        /// <summary>
        /// Constructs the name of a mysql procedure that can be called.
        /// </summary>
        /// <param name="type">A <see cref="QueryType"/> representing a crud operation.</param>
        /// <param name="table">The name of the table the procedure is for.</param>
        /// <returns>The name of a mysql query that can be called.</returns>
        public static string QueryConstructor(QueryType type, string table)
        {
            return string.Format("{0}_{1}_{2}", Prefix, type.ToString().ToLower(), table);
        }

        /// <summary>
        /// Created a full query 'call x(@param_y);' from a <see cref="QueryType"/> crud operation a table name and an optional number of parameters.
        /// </summary>
        /// <param name="type">A <see cref="QueryType"/> representing a crud operation.</param>
        /// <param name="table">The name of the table the query acts on.</param>
        /// <param name="numberOfParams">The number of parameters the query has.</param>
        /// <returns>A full query for the operation and table specified.</returns>
        public static string FullQuery(QueryType type, string table, int numberOfParams = 0)
        {
            var queryName = QueryConstructor(type, table);
            return FullQuery(queryName, numberOfParams);
        }

        /// <summary>
        /// Creating a full 'call x(@param_y);' from a query name and an optional number of parameters.
        /// </summary>
        /// <param name="queryName">The name of the query.</param>
        /// <param name="numberOfParams">The number of parameters the query has.</param>
        /// <returns>A full query with the number of specified parameters.</returns>
        public static string FullQuery(string queryName, int numberOfParams = 0)
        {
            var paramStr = string.Empty;
            for (int i = 0; i < numberOfParams; ++i)
            {
                paramStr += $"@param_{i + 1}";
                if (i + 1 < numberOfParams)
                {
                    paramStr += ", ";
                }
            }

            return string.Format("call {0}({1});", queryName, paramStr);
        }
    }
}
