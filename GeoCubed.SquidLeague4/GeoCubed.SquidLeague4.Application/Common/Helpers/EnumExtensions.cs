using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace GeoCubed.SquidLeague4.Application.Common.Helpers
{
    public static class EnumExtensions
    {
        /// <summary>
        /// Gets the description decorator of a given enum.
        /// </summary>
        /// <param name="anyEnum">An <see cref="Enum"/> to get the description of.</param>
        /// <returns>The enum description or the enum to string if there is no description.</returns>
        public static string GetDescription(this Enum anyEnum)
        {
            Type enumType = anyEnum.GetType();
            MemberInfo[] info = enumType.GetMember(anyEnum.ToString());
            if (info != null && info.Length > 0)
            {
                var attribs = info[0].GetCustomAttributes(typeof(System.ComponentModel.DescriptionAttribute), false);
                if (attribs != null && attribs.Count() > 0)
                {
                    return ((System.ComponentModel.DescriptionAttribute)attribs.ElementAt(0)).Description;
                }
            }

            return anyEnum.ToString();
        }

        public static bool TryGetValueFromDescription<T>(string description, out T enumObj) where T : Enum
        {
            foreach (var field in typeof(T).GetFields())
            {
                if (Attribute.GetCustomAttribute(field,
                    typeof(DescriptionAttribute)) is DescriptionAttribute attribute)
                {
                    if (attribute.Description == description)
                    {
                        enumObj = (T)field.GetValue(null);
                        return true;
                    }
                }
                else
                {
                    if (field.Name == description)
                    {
                        enumObj = (T)field.GetValue(null);
                        return true;
                    }
                }
            }

            enumObj = default(T);
            return false;
        }
    }
}
