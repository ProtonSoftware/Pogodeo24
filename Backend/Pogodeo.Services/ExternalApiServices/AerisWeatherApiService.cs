using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pogodeo.Core;
using System;
using System.Collections.Generic;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service for AerisWeather API calls that gets us weather info
    /// </summary>
    public class AerisWeatherApiService : IAerisWeatherApiService
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

        #region Private JSON Classes

        private class Period
        {
            public int? timestamp { get; set; }
            public DateTime validTime { get; set; }
            public DateTime dateTimeISO { get; set; }
            public int? maxTempC { get; set; }
            public int? maxTempF { get; set; }
            public int? minTempC { get; set; }
            public int? minTempF { get; set; }
            public int? avgTempC { get; set; }
            public int? avgTempF { get; set; }
            public int? tempC { get; set; }
            public int? tempF { get; set; }
            public int? pop { get; set; }
            public double? precipMM { get; set; }
            public double? precipIN { get; set; }
            public object iceaccum { get; set; }
            public object iceaccumMM { get; set; }
            public object iceaccumIN { get; set; }
            public int? maxHumidity { get; set; }
            public int? minHumidity { get; set; }
            public int? humidity { get; set; }
            public int? uvi { get; set; }
            public int? pressureMB { get; set; }
            public double? pressureIN { get; set; }
            public int? sky { get; set; }
            public int? snowCM { get; set; }
            public int? snowIN { get; set; }
            public int? feelslikeC { get; set; }
            public int? feelslikeF { get; set; }
            public int? minFeelslikeC { get; set; }
            public int? minFeelslikeF { get; set; }
            public int? maxFeelslikeC { get; set; }
            public int? maxFeelslikeF { get; set; }
            public int? avgFeelslikeC { get; set; }
            public int? avgFeelslikeF { get; set; }
            public int? dewpointC { get; set; }
            public int? dewpointF { get; set; }
            public int? maxDewpointC { get; set; }
            public int? maxDewpointF { get; set; }
            public int? minDewpointC { get; set; }
            public int? minDewpointF { get; set; }
            public int? avgDewpointC { get; set; }
            public int? avgDewpointF { get; set; }
            public int? windDirDEG { get; set; }
            public string windDir { get; set; }
            public int? windDirMaxDEG { get; set; }
            public string windDirMax { get; set; }
            public int? windDirMinDEG { get; set; }
            public string windDirMin { get; set; }
            public int? windGustKTS { get; set; }
            public int? windGustKPH { get; set; }
            public int? windGustMPH { get; set; }
            public int? windSpeedKTS { get; set; }
            public int? windSpeedKPH { get; set; }
            public int? windSpeedMPH { get; set; }
            public int? windSpeedMaxKTS { get; set; }
            public int? windSpeedMaxKPH { get; set; }
            public int? windSpeedMaxMPH { get; set; }
            public int? windSpeedMinKTS { get; set; }
            public int? windSpeedMinKPH { get; set; }
            public int? windSpeedMinMPH { get; set; }
            public int? windDir80mDEG { get; set; }
            public string windDir80m { get; set; }
            public int? windDirMax80mDEG { get; set; }
            public string windDirMax80m { get; set; }
            public int? windDirMin80mDEG { get; set; }
            public string windDirMin80m { get; set; }
            public int? windGust80mKTS { get; set; }
            public int? windGust80mKPH { get; set; }
            public int? windGust80mMPH { get; set; }
            public int? windSpeed80mKTS { get; set; }
            public int? windSpeed80mKPH { get; set; }
            public int? windSpeed80mMPH { get; set; }
            public int? windSpeedMax80mKTS { get; set; }
            public int? windSpeedMax80mKPH { get; set; }
            public int? windSpeedMax80mMPH { get; set; }
            public int? windSpeedMin80mKTS { get; set; }
            public int? windSpeedMin80mKPH { get; set; }
            public int? windSpeedMin80mMPH { get; set; }
            public string weather { get; set; }
            public List<object> weatherCoded { get; set; }
            public string weatherPrimary { get; set; }
            public string weatherPrimaryCoded { get; set; }
            public string cloudsCoded { get; set; }
            public string icon { get; set; }
            public bool isDay { get; set; }
        }

        private class Profile
        {
            public string tz { get; set; }
        }

        private class Response
        {
            public string interval { get; set; }
            public List<Period> periods { get; set; }
            public Profile profile { get; set; }
        }

        private class RootJsonObject
        {
            public bool success { get; set; }
            public object error { get; set; }
            public List<Response> response { get; set; }
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
        /// The client's ID value to access this API
        /// </summary>
        public string ClientID => mConfigurationSection.GetValue<string>("ClientID");

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
        public AerisWeatherApiService(IConfiguration config, IBigCitiesRepository bigCitiesRepository)
        {
            // Catch the configuration section for this API from configuration
            mConfigurationSection = config.GetSection("AerisWeatherAPI").GetSection("Config");

            // Get repository for big cities
            mBigCitiesRepository = bigCitiesRepository;
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
            // Prepare response model to return
            var response = new WeatherInformationAPIModel
            {
                TodayWeatherTruncatedData = new Dictionary<DateTime, CardHourDataAPIModel>(),
                NextDaysWeatherTruncatedData = new Dictionary<DateTime, CardDayDataAPIModel>()
            };

            #region Today's data

            // Build an url for api request
            var weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherPath, $"{city},pl?filter=1hr&limit=24&client_id={ClientID}&", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            var weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            // Deserialize to json object
            var jsonHourObject = JsonConvert.DeserializeObject<RootJsonObject>(weatherResponseText);

            // If we didn't get any data
            if (!jsonHourObject.success)
                // Return failure
                return new OperationResult<object>(jsonHourObject.success);

            // Collect every weather data
            foreach (var weather in jsonHourObject.response[0].periods)
            {
                // Add new timestamp
                response.TodayWeatherTruncatedData.Add(weather.dateTimeISO, new CardHourDataAPIModel
                {
                    ValueTemperature = weather.avgTempC,
                    ValueRain = weather.precipMM,
                    ValueHumidity = weather.humidity,
                    ValueWind = weather.windSpeedKPH
                });
            }

            #endregion

            #region Next Days Data

            // Build an url for api request
            weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherPath, $"{city},pl?filter=daynight&limit=28&client_id={ClientID}&", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            // Deserialize to json object
            var jsonDayObject = JsonConvert.DeserializeObject<RootJsonObject>(weatherResponseText);

            // If we didn't get any data
            if (!jsonDayObject.success)
                // Return failure
                return new OperationResult<object>(jsonDayObject.success);

            // Collect every weather data
            var weatherObject = new CardDayDataAPIModel();
            foreach (var weather in jsonDayObject.response[0].periods)
            {
                // If we are at day 
                if (weatherObject.DayTemperature == null)
                    // Set the day's data
                    weatherObject.DayTemperature = weather.avgTempC;
                // Otherwise...
                else
                {
                    // Set the night's data
                    weatherObject.NightTemperature = weather.avgTempC;

                    // Add new timestamp
                    response.NextDaysWeatherTruncatedData.Add(weather.dateTimeISO, weatherObject);

                    // Reset the object state
                    weatherObject = new CardDayDataAPIModel();
                }
            }

            #endregion

            // Finally return our response model
            return new OperationResult<object>(true, response);
        }

        #endregion
    }
}
