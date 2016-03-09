using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace Koopakiller.Apps.Brainstorming.Shared.Converter
{
    // ReSharper disable once InconsistentNaming
    public class IPAddressToStringConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var ip = value as IPAddress;
            return ip?.ToString() ?? string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var s = value as string;
            if (s == null) return null;
            IPAddress ip;
            return !IPAddress.TryParse(s, out ip) ? null : ip;
        }
    }
}
