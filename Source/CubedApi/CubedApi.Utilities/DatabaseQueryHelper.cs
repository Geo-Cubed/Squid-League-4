using CubedApi.Utilities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace CubedApi.Utilities
{
    public static class DatabaseQueryHelper
    {
        public static string Prefix = "api";
        public static string SuffixId = "id";
        public static string SuffixDate = "date";

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

        // Special names.
        public static string SetInformation = "set_information";
        public static string UpcommingMatches = "upcomming_matches";
        public static string SwissMatch = "swiss_match";
        public static string KnockoutMatch = "knockout_match";


        /// <summary>
        /// Constructs the name of a mysql procedure that can be called.
        /// </summary>
        /// <param name="type">A <see cref="QueryType"/> representing a crud operation.</param>
        /// <param name="table">The name of the table the procedure is for.</param>
        /// <returns>The name of a mysql query that can be called.</returns>
        public static string QueryConstructor(QueryTypes type, string table)
        {
            return string.Format("{0}_{1}_{2}", Prefix, type.ToString().ToLower(), table);
        }

        public static string BySuffixConstructor(string table, string suffix)
        {
            return string.Format("by_{0}_{1}", table, suffix);
        }

        public static string OnSuffixConstructor(string table)
        {
            return string.Format("on_{0}", table);
        }

        /// <summary>
        /// Created a full query 'call x(@param_y);' from a <see cref="QueryType"/> crud operation a table name and an optional number of parameters.
        /// </summary>
        /// <param name="type">A <see cref="QueryType"/> representing a crud operation.</param>
        /// <param name="table">The name of the table the query acts on.</param>
        /// <param name="suffix">The suffix of the query.</param>
        /// <param name="numberOfParams">The number of parameters the query has.</param>
        /// <returns>A full query for the operation and table specified.</returns>
        public static string FullQuery(QueryTypes type, string table, string suffix, int numberOfParams = 0)
        {
            var queryName = QueryConstructor(type, table);
            if (!string.IsNullOrEmpty(suffix))
            {
                queryName = string.Format("{0}_{1}", queryName, suffix);
            }

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
