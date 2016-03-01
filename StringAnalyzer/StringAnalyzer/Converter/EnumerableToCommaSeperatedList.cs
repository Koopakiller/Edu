using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace StringAnalyzer.Converter
{
    public class EnumerableToCommaSeperatedList : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var val = value as IEnumerable;
            if (val == null)
            {
                throw new ArgumentException("", nameof(value));
            }

            return string.Join(", ", val.Cast<object>());
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
