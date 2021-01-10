using System;
using System.Globalization;
using System.Windows.Data;
using System.Windows.Markup;

namespace SquidLeagueAdmin.Utilities
{
    public class EnumConverter : MarkupExtension, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            try
            {
                var item = (Enum)value;
            }
            catch
            {
                return string.Empty;
            }

            return ((Enum)value).GetDescription();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return this;
        }
    }
}
