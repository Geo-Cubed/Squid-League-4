using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using System.Text.RegularExpressions;

namespace SquidLeagueAdmin.Utilities
{
    public static class Utilities
    {
        /// <summary>
        /// Checks if a string contains only numbers.
        /// </summary>
        /// <param name="input">The string input to check.</param>
        /// <returns>True if only numbers, false otherwise.</returns>
        public static bool IsNumeric(this string input)
        {
            Regex regex = new Regex("[^0-9]+");
            return regex.IsMatch(input);
        }

        /// <summary>
        /// Gets the enum from it's description.
        /// </summary>
        /// <typeparam name="T">An <see cref="Enum"/>.</typeparam>
        /// <param name="description">The description of the enum to find.</param>
        /// <returns>An <see cref="Enum"/> with the description given.</returns>
        public static T GetEnumFromDescription<T>(this string description) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                        return (T)field.GetValue(null);
                }
                else
                {
                    if (field.Name == description)
                        return (T)field.GetValue(null);
                }
            }

            throw new ArgumentException("Not found.", nameof(description));
        }
    }
}
