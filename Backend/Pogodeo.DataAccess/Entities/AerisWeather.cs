using Pogodeo.Core;
using System;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// The entity for AerisWeather cached weather
    /// </summary>
    public class AerisWeather : BaseObject<int>
    {
        #region Public Properties

        /// <summary>
        /// The last date this entry was updated
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// The hourly weather data as JSON text
        /// </summary>
        public string WeatherHourData { get; set; }

        /// <summary>
        /// The daily weather data as JSON text
        /// </summary>
        public string WeatherDayData { get; set; }

        #endregion
    }
}
