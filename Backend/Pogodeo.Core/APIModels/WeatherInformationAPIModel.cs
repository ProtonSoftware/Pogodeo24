using System.Collections.Generic;

namespace Pogodeo.Core
{
    /// <summary>
    /// The API model for weather information for every external API
    /// </summary>
    public class WeatherInformationAPIModel
    {
        /// <summary>
        /// The data about weather from external APIs
        /// </summary>
        public Dictionary<string, CardDataAPIModel> ExternalAPIWeatherData { get; set; }
    }
}
