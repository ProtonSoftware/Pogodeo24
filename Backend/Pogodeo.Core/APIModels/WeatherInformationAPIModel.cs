using System;
using System.Collections.Generic;

namespace Pogodeo.Core
{
    /// <summary>
    /// The API model for weather information for single external API
    /// </summary>
    public class WeatherInformationAPIModel
    {
        /// <summary>
        /// The data about today's weather from external API
        /// Every set of values is associated with specific timestamp of today
        /// </summary>
        public Dictionary<DateTime, CardHourDataAPIModel> TodayWeatherTruncatedData { get; set; }

        /// <summary>
        /// The data about next days weather from external API
        /// Every set of values is associated with day/night of next days
        /// </summary>
        public Dictionary<DateTime, CardDayDataAPIModel> NextDaysWeatherTruncatedData { get; set; }
    }
}
