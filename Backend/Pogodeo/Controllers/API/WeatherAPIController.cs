using Microsoft.AspNetCore.Mvc;
using Pogodeo.Core;
using Pogodeo.Services;
using System;
using System.Collections.Generic;

namespace Pogodeo
{
    /// <summary>
    /// Main weather API controller for this application that returns data to the frontend
    /// </summary>
    public class WeatherAPIController : Controller
    {
        #region Private Members

        /// <summary>
        /// The facade that gets us proper city to check weather for
        /// </summary>
        private readonly ICityFacade mCityFacade;

        /// <summary>
        /// The AccuWeather API service
        /// </summary>
        private readonly IAccuWeatherApiService mAccuWeatherApiService;

        /// <summary>
        /// The Pogodynka.net API service
        /// </summary>
        private readonly IAerisWeatherApiService mAerisWeatherApiService;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherAPIController(ICityFacade cityFacade, IAccuWeatherApiService accuWeatherApiService, IAerisWeatherApiService aerisWeatherApiService)
        {
            mCityFacade = cityFacade;
            mAccuWeatherApiService = accuWeatherApiService;
            mAerisWeatherApiService = aerisWeatherApiService;
        }

        #endregion

        #region API Methods

        /// <summary>
        /// Gets weather information from external APIs by specified city
        /// </summary>
        /// <param name="city">The name of a city to get info for</param>
        /// <returns>JSON</returns>
        [HttpPost]
        [Route(ApiRoutes.GetWeatherForCity)]
        public IActionResult GetWeatherForCity([FromBody]string city)
        {
            // Get the city that we will get weather for
            var weatherCity = mCityFacade.GetWeatherCity(city);

            // If none was found
            if (weatherCity == null)
                return NotFound();

            // Get weather from AccuWeather
            var accuResponse = mAccuWeatherApiService.GetAPIInfo(weatherCity).Result as WeatherInformationAPIModel;

            // Get weather from AerisWeather
            var aerisResponse = mAerisWeatherApiService.GetAPIInfo(weatherCity).Result as WeatherInformationAPIModel;

            // Create response object
            var response = new APIWeatherResponse
            {
                WeatherResponses = new Dictionary<string, WeatherInformationAPIModel>
                {
                    { "AccuWeather", accuResponse },
                    { "AerisWeather", aerisResponse }
                }
            };

            // Return successful response with data
            return Ok(response);
        }

        #endregion
    }
}
