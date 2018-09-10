using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pogodeo.Core;
using System.Collections.Generic;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service that gets location information from OpenCageGeocoder API
    /// </summary>
    public class OpenCageGeocoderService : IOpenCageGeocoderService
    {
        #region Private Members

        /// <summary>
        /// Holds the section for configuration for this API
        /// </summary>
        private readonly IConfigurationSection mConfigurationSection;

        #endregion

        #region Private JSON Classes

        private class Rate
        {
            public int limit { get; set; }
            public int remaining { get; set; }
            public int reset { get; set; }
        }

        private class Geometry
        {
            public double lat { get; set; }
            public double lng { get; set; }
        }

        private class Result
        {
            public Geometry geometry { get; set; }
        }

        private class Status
        {
            public int code { get; set; }
            public string message { get; set; }
        }

        private class RootJsonObject
        {
            public Rate rate { get; set; }
            public List<Result> results { get; set; }
            public Status status { get; set; }
        }

        #endregion

        #region Public Properties

    /// <summary>
    /// The host url of this API
    /// </summary>
    public string Host => mConfigurationSection.GetValue<string>("Host");

        /// <summary>
        /// The API Key name that is used in an url for this API
        /// </summary>
        public string ApiKeyName => mConfigurationSection.GetSection("APIKey").GetValue<string>("Name");

        /// <summary>
        /// The API Key value to access this API
        /// </summary>
        public string ApiKeyValue => mConfigurationSection.GetSection("APIKey").GetValue<string>("Value");

        /// <summary>
        /// The path prefix to this API
        /// </summary>
        public string CityInfoPath => mConfigurationSection.GetValue<string>("PathPrefix");

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config">The config for this app containing API info</param>
        public OpenCageGeocoderService(IConfiguration config)
        {
            // Catch the configuration section for this API from configuration
            mConfigurationSection = config.GetSection("TheOpenCageGeocoderAPI").GetSection("Config");
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Makes a request to this API to get informations about specified city
        /// </summary>
        /// <param name="city">The name of a city to send request for</param>
        /// <returns>API response model or failure</returns>
        public OperationResult<object> GetAPIInfo(string city)
        {
            // Build url for API request
            var apiUrl = ExternalApiServiceHelpers.BuildUrl(Host, CityInfoPath, $"?q={city}&", ApiKeyName, ApiKeyValue);

            // Do the request
            var apiResponseText = ExternalApiServiceHelpers.SendAPIRequest(apiUrl);

            // Get response as json
            var jsonObject = JsonConvert.DeserializeObject<RootJsonObject>(apiResponseText);

            // If we didn't get any data
            if (jsonObject.status.code != 200)
                // Return failure
                return new OperationResult<object>(false);

            // Otherwise, create our API response model
            var responseModel = new OpenCageGeocoderCityModel
            {
                Name = city,
                Latitude = jsonObject.results[0].geometry.lat.ToString(),
                Longitude = jsonObject.results[0].geometry.lng.ToString()
            };

            // Return the model
            return new OperationResult<object>(true, responseModel);
        }

        #endregion
    }
}
