using Pogodeo.Core;

namespace Pogodeo
{
    /// <summary>
    /// The view model for show weather page
    /// </summary>
    public class ShowWeatherViewModel
    {
        /// <summary>
        /// The name of city that user has provided
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The response from API that should be shown in this page
        /// </summary>
        public APIWeatherResponse APIResponse { get; set; }
    }
}
