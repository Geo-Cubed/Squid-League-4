using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace SquidLeagueAdmin.Utilities
{
    public static class EnumExtentions
    {
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
    }
}
