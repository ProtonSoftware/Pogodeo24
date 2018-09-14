using System;
using System.Collections.Generic;
using Pogodeo.Core;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The context for city weather data
    /// </summary>
    public class CityWeatherContext
    {
        /// <summary>
        /// The name of a city this weather is associated with
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The last date this weather was updated
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        /// <summary>
        /// The current cached weather data for this city
        /// </summary>
        public Dictionary<APIProviderType, WeatherInformationAPIModel> Weather { get; set; }
    }
}
