using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace timely.common.Infrastructure
{
    public class TimeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value ?? DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return DependencyProperty.UnsetValue;
            }

            var strValue = value.ToString();
            TimeSpan resultTimeSpan;
            return TimeSpan.TryParse(strValue, out resultTimeSpan) ? new TimeSpan?(resultTimeSpan) : null;
        }
    }
}