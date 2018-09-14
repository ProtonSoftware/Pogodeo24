using Pogodeo.Core;
using System;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The entity for cached weather for cities
    /// </summary>
    public class CityWeather : BaseObject<int>
    {
        /// <summary>
        /// The name of a city the weather is associated with
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The last date that this entity was updated
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// The saved weather data for this city as json string
        /// </summary>
        public string WeatherData { get; set; }
    }
}
