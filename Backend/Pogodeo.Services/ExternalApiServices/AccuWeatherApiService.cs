using Microsoft.Extensions.Configuration;
using Newtonsoft.Json.Linq;
using Pogodeo.Core;
using System.Collections.Generic;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service for AccuWeather API calls that gets us weather info
    /// </summary>
    public class AccuWeatherApiService : IAccuWeatherApiService
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
        /// The path prefix to localization key API
        /// </summary>
        public string LocalizationKeyPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("LocalizationKey");

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
        public AccuWeatherApiService(IConfiguration config, IBigCitiesRepository bigCitiesRepository)
        {
            // Catch the configuration section for this API from configuration
            mConfigurationSection = config.GetSection("AccuWeatherAPI").GetSection("Config");

            // Get repository for big cities
            mBigCitiesRepository = bigCitiesRepository;
        }

        #endregion

        #region Public Methods

        public OperationResult<object> GetAPIInfo(string city)
        {
            // Try to get localization key from database
            var localizationKey = mBigCitiesRepository.GetAccuWeatherLocalizationKey(city);

            // If we didn't get any
            if (localizationKey == string.Empty || localizationKey == null)
                localizationKey = GetLocalizationKeyFromApi(city);

            // At this point, we got our localization key
            // Build an url for api request based on that
            var weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherPath, $"{localizationKey}?", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            var weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            var jsonObject = JObject.Parse(weatherResponseText);
            var jArray = JArray.Parse(jsonObject["DailyForecasts"].ToString());

            var weatherlist = new List<AccuWeatherWeatherModel>();

            foreach (var json in jArray)
            {
                var weatherInfo = json.ToObject<AccuWeatherWeatherModel>();
                weatherInfo.Maximum = int.Parse(json["Temperature"]["Maximum"]["Value"].ToString());
                weatherInfo.Minimum = int.Parse(json["Temperature"]["Minimum"]["Value"].ToString());
                weatherlist.Add(weatherInfo);
            }

            return new OperationResult<object>(true, weatherlist);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets localization key by making an API call
        /// In ideal case, shouldn't be called at all
        /// </summary>
        /// <param name="city">The city to get localization key for</param>
        /// <returns>Localization key as string</returns>
        private string GetLocalizationKeyFromApi(string city)
        {
            // Build an url for api request
            var localizationUrl = ExternalApiServiceHelpers.BuildUrl(Host, LocalizationKeyPath, $"?q={city}&", ApiKeyName, ApiKeyValue);

            // Send that request and catch json response
            var localizationResponseText = ExternalApiServiceHelpers.SendAPIRequest(localizationUrl);

            // The response is always a json array, parse it as that
            var jsonArray = JArray.Parse(localizationResponseText);

            // Deserialize the first array object, we don't care about duplicates
            var localizationKeyObject = jsonArray[0].ToObject<AccuWeatherLocalizationKeyModel>();

            // Update localization key to the database for future requests
            mBigCitiesRepository.UpdateAccuWeatherLocalizationKey(city, localizationKeyObject.Key);

            // Return found localization key
            return localizationKeyObject.Key;
        }

        #endregion
    }
}
