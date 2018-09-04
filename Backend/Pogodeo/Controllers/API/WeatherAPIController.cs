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

        private readonly ITestService _service;
        private readonly IInteriaWeatherApiService _interiaWeatherApiService;
        private readonly IOpenCageGeocoder _geo;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherAPIController(ITestService service, IOpenCageGeocoder geo, IInteriaWeatherApiService interiaWeatherApiService)
        {
            _service = service;
            _geo = geo;
            _interiaWeatherApiService = interiaWeatherApiService;
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
            var response = new APIWeatherResponse
            {
                AggregatedWeatherList = new Dictionary<DateTime, WeatherInformationAPIModel>
                {
                    {
                        new DateTime(2018, 11, 20, 15, 00, 00, 00), new WeatherInformationAPIModel
                        {
                            ExternalAPIWeatherData = new Dictionary<string, CardDataAPIModel>
                            {
                                {
                                    "Onet", new CardDataAPIModel
                                    {
                                        ValueTemperature = 20,
                                        ValueHumidity = 50,
                                        ValueRain = 20,
                                        ValueWind = 9
                                    }
                                },
                                {
                                    "WP", new CardDataAPIModel
                                    {
                                        ValueTemperature = 22,
                                        ValueHumidity = 40,
                                        ValueRain = 10,
                                        ValueWind = 19
                                    }
                                },
                                {
                                    "Interia", new CardDataAPIModel
                                    {
                                        ValueTemperature = 29,
                                        ValueHumidity = 59,
                                        ValueRain = 11,
                                        ValueWind = 11
                                    }
                                }
                            }
                        }
                    },
                    {
                        new DateTime(2018, 11, 20, 18, 00, 00, 00), new WeatherInformationAPIModel
                        {
                            ExternalAPIWeatherData = new Dictionary<string, CardDataAPIModel>
                            {
                                {
                                    "Onet", new CardDataAPIModel
                                    {
                                        ValueTemperature = 21,
                                        ValueHumidity = 51,
                                        ValueRain = 21,
                                        ValueWind = 9
                                    }
                                },
                                {
                                    "WP", new CardDataAPIModel
                                    {
                                        ValueTemperature = 23,
                                        ValueHumidity = 41,
                                        ValueRain = 11,
                                        ValueWind = 19
                                    }
                                },
                                {
                                    "Interia", new CardDataAPIModel
                                    {
                                        ValueTemperature = 33,
                                        ValueHumidity = 59,
                                        ValueRain = 12,
                                        ValueWind = 11
                                    }
                                }
                            }
                        }
                    }
                }
            };

            // Return successful response with data
            return Ok(response);
        }

        #endregion
    }
}
