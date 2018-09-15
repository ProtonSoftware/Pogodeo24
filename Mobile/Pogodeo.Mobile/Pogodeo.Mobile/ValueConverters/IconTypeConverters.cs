using Pogodeo.Core;
using System;
using System.Globalization;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// A converter that takes in a <see cref="WeatherIconType"/> 
    /// and returns the Weather Icon Font string for that icon
    /// </summary>
    public class WeatherIconTypeToFontConverter : BaseValueConverter<WeatherIconTypeToFontConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((WeatherIconType)value).ToWeatherFont();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// A converter that takes in a <see cref="ApplicationIconType"/> 
    /// and returns the FontAwesome string for that icon
    /// </summary>
    public class ApplicationIconTypeToFontAwesomeConverter : BaseValueConverter<ApplicationIconTypeToFontAwesomeConverter>
    {
        public override object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return ((ApplicationIconType)value).ToMaterialFont();
        }

        public override object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}