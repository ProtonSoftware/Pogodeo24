using System.Collections.Generic;

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
        /// The list of informations about weather from external APIs
        /// </summary>
        public List<WeatherInformationViewModel> WeatherInformationsList { get; set; }
    }
}
