using Pogodeo.Core;
using System;
using System.Collections.Generic;

namespace Pogodeo.Services
{
    /// <summary>
    /// The context for weather info
    /// </summary>
    public class WeatherContext
    {
        #region Public Properties

        /// <summary>
        /// The last date this entry was updated
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

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

        #endregion
    }
}
