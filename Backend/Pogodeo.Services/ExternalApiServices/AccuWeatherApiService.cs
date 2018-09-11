using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Pogodeo.Core;
using System;
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

        #region Private JSON Classes

        private class AccuWeatherLocalizationKeyModel
        {
            public string LocalizedName { get; set; }
            public string Key { get; set; }
        }

        private class Headline
        {
            public DateTime EffectiveDate { get; set; }
            public int EffectiveEpochDate { get; set; }
            public int Severity { get; set; }
            public string Text { get; set; }
            public string Category { get; set; }
            public DateTime EndDate { get; set; }
            public int EndEpochDate { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }
        }

        private class Wind
        {
            public Minimum Speed { get; set; }
        }

        private class Minimum
        {
            public double? Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

        private class Maximum
        {
            public double? Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
        }

        private class Temperature
        {
            public double? Value { get; set; }
            public string Unit { get; set; }
            public int UnitType { get; set; }
            public Minimum Minimum { get; set; }
            public Maximum Maximum { get; set; }
        }

        private class Day
        {
            public int Icon { get; set; }
            public string IconPhrase { get; set; }
        }

        private class Night
        {
            public int Icon { get; set; }
            public string IconPhrase { get; set; }
        }

        private class DailyForecast
        {
            public DateTime Date { get; set; }
            public int EpochDate { get; set; }
            public Temperature Temperature { get; set; }
            public Day Day { get; set; }
            public Night Night { get; set; }
            public List<string> Sources { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }
        }

        private class RootJsonDayObject
        {
            public Headline Headline { get; set; }
            public List<DailyForecast> DailyForecasts { get; set; }
        }

        private class RootJsonHourObject
        {
            public DateTime DateTime { get; set; }
            public int EpochDateTime { get; set; }
            public int WeatherIcon { get; set; }
            public string IconPhrase { get; set; }
            public bool IsDaylight { get; set; }
            public Temperature Temperature { get; set; }
            public Wind Wind { get; set; }
            public int RelativeHumidity { get; set; }
            public int UVIndex { get; set; }
            public string UVIndexText { get; set; }
            public int PrecipitationProbability { get; set; }
            public int RainProbability { get; set; }
            public int SnowProbability { get; set; }
            public int IceProbability { get; set; }
            public Minimum TotalLiquid { get; set; }
            public Minimum Rain { get; set; }
            public int CloudCover { get; set; }
            public string MobileLink { get; set; }
            public string Link { get; set; }
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
        /// The path prefix to localization key API
        /// </summary>
        public string LocalizationKeyPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("LocalizationKey");

        /// <summary>
        /// The path prefix to hourly weather info API
        /// </summary>
        public string WeatherHourPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("WeatherHour");

        /// <summary>
        /// The path prefix to daily weather info API
        /// </summary>
        public string WeatherDayPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("WeatherDay");

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

        /// <summary>
        /// Makes a request to this API to get informations about specified city
        /// </summary>
        /// <param name="city">The name of a city to send request for</param>
        /// <returns>API response model or failure</returns>
        public OperationResult<WeatherInformationAPIModel> GetAPIInfo(string city)
        {
            // Prepare response model to return
            var response = new WeatherInformationAPIModel
            {
                TodayWeatherTruncatedData = new Dictionary<DateTime, CardHourDataAPIModel>(),
                NextDaysWeatherTruncatedData = new Dictionary<DateTime, CardDayDataAPIModel>()
            };

            // Try to get localization key from database
            var localizationKey = mBigCitiesRepository.GetAccuWeatherLocalizationKey(city);

            // If we didn't get any
            if (localizationKey == string.Empty || localizationKey == null)
                // Get one from API
                localizationKey = GetLocalizationKeyFromApi(city);

            #region Today's Data

            // Build an url for api request based on localization key
            var weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherHourPath, $"{localizationKey}?details=true&", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            var weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            // Deserialize to json object
            var jsonHourObject = JsonConvert.DeserializeObject<List<RootJsonHourObject>>(weatherResponseText);

            // If we didn't get any data
            if (jsonHourObject == null)
                // Return failure
                return new OperationResult<WeatherInformationAPIModel>(false);

            // Collect every weather data
            foreach (var weather in jsonHourObject)
            {
                // Add new timestamp
                response.TodayWeatherTruncatedData.Add(weather.DateTime, new CardHourDataAPIModel
                {
                    ValueTemperature = (int)Math.Round((double)((weather.Temperature.Value - 32) * 5 / 9), 0),
                    ValueRain = weather.Rain.Value,
                    ValueHumidity = weather.RelativeHumidity,
                    ValueWind = (int)Math.Round((double)weather.Wind.Speed.Value, 0)
                });
            }

            #endregion

            #region Next Days Data

            // Build an url for api request based on localization key
            weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherDayPath, $"{localizationKey}?", ApiKeyName, ApiKeyValue);

            // Catch the response from external api
            weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);

            // Deserialize to json object
            var jsonDayObject = JsonConvert.DeserializeObject<RootJsonDayObject>(weatherResponseText);

            // If we didn't get any data
            if (jsonDayObject == null)
                // Return failure
                return new OperationResult<WeatherInformationAPIModel>(false);

            // Collect every weather data
            foreach (var weather in jsonDayObject.DailyForecasts)
            {
                // Add new timestamp
                response.NextDaysWeatherTruncatedData.Add(weather.Date, new CardDayDataAPIModel
                {
                    DayTemperature = (int)Math.Round((double)((weather.Temperature.Maximum.Value - 32) * 5 / 9), 0),
                    NightTemperature = (int)Math.Round((double)((weather.Temperature.Minimum.Value - 32) * 5 / 9), 0)
                });
            }

            #endregion

            // Finally return our response model
            return new OperationResult<WeatherInformationAPIModel>(true, response);
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
