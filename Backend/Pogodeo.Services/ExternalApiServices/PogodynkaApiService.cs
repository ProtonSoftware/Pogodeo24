using Microsoft.Extensions.Configuration;
using Pogodeo.Core;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service for Pogodynka.net API calls that gets us weather info
    /// </summary>
    public class PogodynkaApiService : IPogodynkaApiService
    {
        #region Private Members

        /// <summary>
        /// Holds the section for configuration for this API
        /// </summary>
        private readonly IConfigurationSection mConfigurationSection;

        /// <summary>
        /// The repository to access big cities data from database
        /// </summary>
        private readonly IBigCitiesRepository mBigCitiesRepository;

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
        /// The path prefix to weather info API
        /// </summary>
        public string WeatherPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("Weather");

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config">The config for this app containing API info</param>
        public PogodynkaApiService(IConfiguration config, IBigCitiesRepository bigCitiesRepository)
        {
            // Catch the configuration section for this API from configuration
            mConfigurationSection = config.GetSection("PogodynkaAPI").GetSection("Config");

            // Get repository for big cities
            mBigCitiesRepository = bigCitiesRepository;
        }

        #endregion

        #region Public Methods

        public OperationResult<object> GetAPIInfo(string city)
        {
            // Build an url for api reques
            var weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherPath, $"", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            var weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            return null;
        }

        #endregion
    }
}
