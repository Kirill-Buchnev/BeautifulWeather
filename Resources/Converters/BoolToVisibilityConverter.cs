﻿using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;

namespace BeautifulWeather.Resources.Converters
{
    public class BoolToVisibilityConverter : IValueConverter
    {
        public Visibility TrueVisibility { get; set; }
        public Visibility FalseVisibility { get; set; }

        public BoolToVisibilityConverter()
        {
            TrueVisibility = Visibility.Visible;
            FalseVisibility = Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return (bool)value ? TrueVisibility : FalseVisibility;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
