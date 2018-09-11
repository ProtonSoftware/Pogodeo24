using System.Collections.Generic;

namespace Pogodeo.Core
{
    /// <summary>
    /// The model for API weather response object
    /// </summary>
    public class APIWeatherResponse
    {
        /// <summary>
        /// The list of every weather response from external APIs with truncated data
        /// </summary>
        public Dictionary<APIProviderType, WeatherInformationAPIModel> WeatherResponses { get; set; }
    }
}
