using Microsoft.AspNetCore.Mvc;
using Pogodeo.Services;
using Pogodeo.Services.ExternalApiServices;
using System.Collections.Generic;

namespace Pogodeo
{
    /// <summary>
    /// Main weather API controller for this application that returns data to the frontend
    /// </summary>
    [Route("api")]
    public class WeatherAPIController : Controller
    {
        #region Private Members

        private readonly ITestService _service;
        private readonly IOpenCageGeocoder _geo;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherAPIController(ITestService service, IOpenCageGeocoder geo)
        {
            _service = service;
            _geo = geo;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Gets weather information from external APIs by specified city
        /// </summary>
        /// <param name="city">The name of a city to get info for</param>
        /// <returns>JSON</returns>
        [HttpPost]
        [Route("GetWeatherFor")]
        public IActionResult GetWeatherForCity([FromBody]string city)
        {
            // Check if we have info about this city in database

            // Check if city exists
            var geoResponse = _geo.GetAddressLocation(city);

            // If it does not
            if (false/*!city.DoesExist*/)
                return NotFound();

            // Get weather from Onet

            // Get weather from XXX

            // Get weather from YYY

            // Create response object
            var response = new ShowWeatherViewModel
            {
                CityName = city,
                WeatherInformationsList = new List<WeatherInformationViewModel>
                {
                    new WeatherInformationViewModel
                    {
                        WeatherProviderAPIName = "Onet",
                        Celsius = 20,
                    },
                    new WeatherInformationViewModel
                    {
                        WeatherProviderAPIName = "WP",
                        Celsius = 21,
                    }
                }
            };

            // Return successful response with data
            return Ok(response);
        }

        #endregion
    }
}
