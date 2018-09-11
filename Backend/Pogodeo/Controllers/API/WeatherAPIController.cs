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
        /// The mapper for city objects
        /// </summary>
        private readonly CityMapper mCityMapper;

        /// <summary>
        /// The facade that handles city providing and weather updating
        /// </summary>
        private readonly ICityFacade mCityFacade;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherAPIController(ICityFacade cityFacade, CityMapper cityMapper)
        {
            mCityFacade = cityFacade;
            mCityMapper = cityMapper;
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
            // If we have out-dated weather info, update it
            var result = mCityFacade.UpdateWeatherIfNecessery(city);

            // If no city was found
            if (!result.Success)
                return NotFound();

            // Get up-to-date weather for this city
            var weatherCity = mCityFacade.GetWeatherCity(city);

            // Create response object
            var response = new APIWeatherResponse
            {
                WeatherResponses = new Dictionary<APIProviderType, WeatherInformationAPIModel>
                {
                    { APIProviderType.AccuWeather, mCityMapper.Map(weatherCity.AccuWeatherContext) },
                    { APIProviderType.AerisWeather, mCityMapper.Map(weatherCity.AerisWeatherContext) }
                }
            };

            // Return successful response with data
            return Ok(response);
        }

        /// <summary>
        /// Checks if provided weather's date is outdated or not and requires update
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True or false</returns>
        [HttpPost]
        [Route(ApiRoutes.CheckIfWeatherRequiresUpdate)]
        public IActionResult CheckIfWeatherRequiresUpdate([FromBody]DateTime date)
        {
            // Check the date
            var result = mCityFacade.CheckWeatherDate(date);

            // Return the result
            return Ok(result);
        }

        #endregion
    }
}
