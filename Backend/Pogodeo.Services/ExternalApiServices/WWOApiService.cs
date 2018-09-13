using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Pogodeo.Core;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;

namespace Pogodeo.Services
{
    /// <summary>
    /// The service for WWO (WorldWeatherOnline) API calls that gets us weather info
    /// </summary>
    public class WWOApiService : IWWOApiService
    {
        #region Private Members

        /// <summary>
        /// Holds the section for configuration for this API
        /// </summary>
        private readonly IConfigurationSection mConfigurationSection;

        #endregion

        #region Private JSON Classes

        private class WeatherValue
        {
            public string value { get; set; }
        }

        private class Astronomy
        {
            public string sunrise { get; set; }
            public string sunset { get; set; }
            public string moonrise { get; set; }
            public string moonset { get; set; }
            public string moon_phase { get; set; }
            public string moon_illumination { get; set; }
        }

        private class Hourly
        {
            public string time { get; set; }
            public string tempC { get; set; }
            public string tempF { get; set; }
            public string windspeedMiles { get; set; }
            public string windspeedKmph { get; set; }
            public string winddirDegree { get; set; }
            public string winddir16Point { get; set; }
            public string weatherCode { get; set; }
            public List<WeatherValue> weatherIconUrl { get; set; }
            public List<WeatherValue> weatherDesc { get; set; }
            public string precipMM { get; set; }
            public string humidity { get; set; }
            public string visibility { get; set; }
            public string pressure { get; set; }
            public string cloudcover { get; set; }
            public string HeatIndexC { get; set; }
            public string HeatIndexF { get; set; }
            public string DewPointC { get; set; }
            public string DewPointF { get; set; }
            public string WindChillC { get; set; }
            public string WindChillF { get; set; }
            public string WindGustMiles { get; set; }
            public string WindGustKmph { get; set; }
            public string FeelsLikeC { get; set; }
            public string FeelsLikeF { get; set; }
            public string chanceofrain { get; set; }
            public string chanceofremdry { get; set; }
            public string chanceofwindy { get; set; }
            public string chanceofovercast { get; set; }
            public string chanceofsunshine { get; set; }
            public string chanceoffrost { get; set; }
            public string chanceofhightemp { get; set; }
            public string chanceoffog { get; set; }
            public string chanceofsnow { get; set; }
            public string chanceofthunder { get; set; }
        }

        private class Weather
        {
            public string date { get; set; }
            public List<Astronomy> astronomy { get; set; }
            public string maxtempC { get; set; }
            public string maxtempF { get; set; }
            public string mintempC { get; set; }
            public string mintempF { get; set; }
            public string totalSnow_cm { get; set; }
            public string sunHour { get; set; }
            public string uvIndex { get; set; }
            public List<Hourly> hourly { get; set; }
        }

        private class Data
        {
            public List<Weather> weather { get; set; }
        }

        private class RootJsonObject
        {
            public Data data { get; set; }
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
        /// The path prefix to weather info API
        /// </summary>
        public string WeatherPath => mConfigurationSection.GetSection("PathPrefix").GetValue<string>("Weather");

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="config">The config for this app containing API info</param>
        public WWOApiService(IConfiguration config)
        {
            // Catch the configuration section for this API from configuration
            mConfigurationSection = config.GetSection("WWOAPI").GetSection("Config");
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

            // Prepare container for web responses
            var weatherResponseText = default(string);

            #region API Request

            // Convert city to normalized version - This API works only with english letters
            city = city.ToNormalizedString();

            // Build an url for api request
            var weatherUrl = ExternalApiServiceHelpers.BuildUrl(Host, WeatherPath, $"?q={city}&format=json&", ApiKeyName, ApiKeyValue);

            try
            {
                // Catch the response from external api
                weatherResponseText = ExternalApiServiceHelpers.SendAPIRequest(weatherUrl);
            }
            catch (Exception ex)
            {
                // Something went wrong, return error
                return new OperationResult<WeatherInformationAPIModel>(ex.Message);
            }

            // Deserialize to json object
            var jsonObject = JsonConvert.DeserializeObject<RootJsonObject>(weatherResponseText);

            // If we didn't get any data
            if (jsonObject == null || jsonObject.data == null || jsonObject.data.weather == null)
                // Return error
                return new OperationResult<WeatherInformationAPIModel>("No data in response.");

            #endregion

            #region Today's Data

            // Get first day's data
            var firstDay = jsonObject.data.weather[0];
            
            // For each provided hour...
            foreach (var weather in firstDay.hourly)
            {
                // Add new timestamp
                response.TodayWeatherTruncatedData.Add(DateTime.Today + new TimeSpan(int.Parse(weather.time) / 100, 0, 0), new CardHourDataAPIModel
                {
                    ValueTemperature = int.Parse(weather.tempC),
                    ValueRain = double.Parse(weather.precipMM),
                    ValueHumidity = int.Parse(weather.humidity),
                    ValueWind = int.Parse(weather.windspeedKmph)
                });
            }

            #endregion

            #region Next Days Data

            // Get data for 5 days
            for (var i = 0; i < 5; i++)
            {
                // Get current weather data
                var weather = jsonObject.data.weather[i];

                // Add new timestamp
                response.NextDaysWeatherTruncatedData.Add(DateTime.ParseExact(weather.date, "yyyy-MM-dd", CultureInfo.InvariantCulture), new CardDayDataAPIModel
                {
                    DayTemperature = int.Parse(weather.hourly.Where(x => x.time == "1200").FirstOrDefault().tempC),
                    NightTemperature = int.Parse(weather.hourly.Where(x => x.time == "2100").FirstOrDefault().tempC)
                });   
            }

            #endregion

            // Finally return our response model
            return new OperationResult<WeatherInformationAPIModel>(true, response);
        }

        #endregion
    }
}
