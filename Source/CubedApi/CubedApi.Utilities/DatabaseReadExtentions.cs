using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace CubedApi.Utilities
{
    public static class DatabaseReadExtentions
    {
        /// <summary>
        /// Extentionm of the IDataReader interface to allow null checks and grabing value in one go.
        /// </summary>
        /// <param name="read">The IDataReader related to the query</param>
        /// <param name="name">The name of the column</param>
        /// <param name="value">The out value of the int in the coulmn</param>
        /// <returns>True if value is an int, false if it is DBNull</returns>
        public static bool TryGetValue(this IDataReader read, string name, out int? value)
        {
            value = null;
            if (read.IsClosed)
            {
                return false;
            }

            var ordinal = read.GetOrdinal(name);
            if (read.IsDBNull(ordinal))
            {
                return false;
            }

            value = read.GetInt32(ordinal);
            return true;
        }

        /// <summary>
        /// Extention of the IDataReader interface to allow null checks and grabbing a value in one go.
        /// </summary>
        /// <param name="read">The IDataReader related to the query</param>
        /// <param name="name">The name of the column</param>
        /// <param name="value">Value of the string in the column</param>
        /// <returns>True if the value exists, false is it is DBNull</returns>
        public static bool TryGetValue(this IDataReader read, string name, out string value)
        {
            value = null;
            if (read.IsClosed)
            {
                return false;
            }

            var ordinal = read.GetOrdinal(name);
            if (read.IsDBNull(ordinal))
            {
                return false;
            }

            value = read.GetString(ordinal);
            return true;
        }

        /// <summary>
        /// Extention of the IDataReader interface to allow null checks and grabbing a value in one go.
        /// </summary>
        /// <param name="read">The IDataReader related to the query</param>
        /// <param name="name">The name of the column</param>
        /// <param name="value">Value of the double in the column</param>
        /// <returns>True if the value exists, false if it is DBNull</returns>
        public static bool TryGetValue(this IDataReader read, string name, out double? value)
        {
            value = null;
            if (read.IsClosed)
            {
                return false;
            }

            var ordinal = read.GetOrdinal(name);
            if (read.IsDBNull(ordinal))
            {
                return false;
            }

            value = read.GetDouble(ordinal);
            return true;
        }
    }
}
