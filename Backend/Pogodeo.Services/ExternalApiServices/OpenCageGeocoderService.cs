using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pogodeo.Core;
using System;
using System.Net;

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

        public OperationResult<object> GetAPIInfo(string city)
        {
            var apiUrl = ExternalApiServiceHelpers.BuildUrl(Host, CityInfoPath, $"?q={city}&", ApiKeyName, ApiKeyValue);
            var apiResponseText = ExternalApiServiceHelpers.SendAPIRequest(apiUrl);
            var jsonObject = JsonConvert.DeserializeObject(apiResponseText);
            return new OperationResult<object>(true, jsonObject);
        }

        #endregion
    }
}
