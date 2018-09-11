using Pogodeo.Core;
using System;
using Microsoft.Extensions.Configuration;

namespace Pogodeo.Services
{
    /// <summary>
    /// The facade that handles city operations such as associating small cities with big, database calls etc.
    /// </summary>
    public class CityFacade : ICityFacade
    {
        #region Private Members

        /// <summary>
        /// The mapper for city objects
        /// </summary>
        private readonly CityMapper mCityMapper;

        /// <summary>
        /// The configuration for weather date settings
        /// </summary>
        private readonly IConfiguration mConfiguration;

        /// <summary>
        /// The service that gets us city information from OpenCageGeocoder API
        /// </summary>
        private readonly IOpenCageGeocoderService mOpenCageGeocoderService;

        /// <summary>
        /// The repository for big cities database table
        /// </summary>
        private readonly IBigCitiesRepository mBigCitiesRepository;

        /// <summary>
        /// The repository for small cities database table
        /// </summary>
        private readonly ISmallCitiesRepository mSmallCitiesRepository;

        /// <summary>
        /// The AccuWeather API service
        /// </summary>
        private readonly IAccuWeatherApiService mAccuWeatherApiService;

        /// <summary>
        /// The AerisWeather API service
        /// </summary>
        private readonly IAerisWeatherApiService mAerisWeatherApiService;

        /// <summary>
        /// The WWO (WorldWeatherOnline) API service
        /// </summary>
        private readonly IWWOApiService mWWOApiService;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CityFacade(CityMapper cityMapper,
                          IConfiguration configuration, 
                          IOpenCageGeocoderService openCageGeocoderService, 
                          IBigCitiesRepository bigCitiesRepository, 
                          ISmallCitiesRepository smallCitiesRepository, 
                          IAccuWeatherApiService accuWeatherApiService, 
                          IAerisWeatherApiService aerisWeatherApiService,
                          IWWOApiService wwoApiService)
        {
            mCityMapper = cityMapper;
            mConfiguration = configuration;
            mOpenCageGeocoderService = openCageGeocoderService;
            mBigCitiesRepository = bigCitiesRepository;
            mSmallCitiesRepository = smallCitiesRepository;
            mAccuWeatherApiService = accuWeatherApiService;
            mAerisWeatherApiService = aerisWeatherApiService;
            mWWOApiService = wwoApiService;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the big city that we should get weather for
        /// </summary>
        /// <param name="city">The user's provided city</param>
        /// <returns>The big city from our database as string</returns>
        public BigCityContext GetWeatherCity(string city)
        {
            // Check if we have this city in our big city database
            var bigCity = mBigCitiesRepository.GetByName(city);
            
            // If none was found
            if (bigCity == null)
            {
                // Check if we have this city in our small city database
                var smallCity = mSmallCitiesRepository.GetByName(city);

                // If none was found
                if (smallCity == null)
                {
                    // Send Api request
                    SendApiRequest(city);

                    // Get newly saved small city
                    smallCity = mSmallCitiesRepository.GetByName(city);

                    // If none was found
                    if (smallCity == null)
                        // City doesn't exist
                        return null;
                }

                // Return it's associated big city
                return smallCity.AssociatedBigCity;
            }

            // Otherwise, return it
            return bigCity;
        }

        /// <summary>
        /// Checks if the weather for specified city is up-to-date
        /// In case not, updates the weather with APIs
        /// </summary>
        /// <param name="city">The name of a city to update weather for</param>
        public OperationResult UpdateWeatherIfNecessery(string city)
        {
            // Get the city that we will get weather for
            var weatherCity = GetWeatherCity(city);

            // If none was found
            if (weatherCity == null)
                // Return failure
                return new OperationResult(false);

            // Check if its weather is up-to-date
            if (CheckWeatherDate(weatherCity.AccuWeatherContext.LastUpdateDate))
                // Return success
                return new OperationResult(true);

            // Weather is out-dated
            // Get weather from AccuWeather
            var accuResponse = mAccuWeatherApiService.GetAPIInfo(weatherCity.CityName).Result;
            weatherCity.AccuWeatherContext = mCityMapper.Map(accuResponse);

            // Get weather from AerisWeather
            var aerisResponse = mAerisWeatherApiService.GetAPIInfo(weatherCity.CityName).Result;
            weatherCity.AerisWeatherContext = mCityMapper.Map(aerisResponse);

            // Get weather from WWO
            var wwoResponse = mWWOApiService.GetAPIInfo(weatherCity.CityName).Result;
            weatherCity.WWOContext = mCityMapper.Map(wwoResponse);

            // Save new info to the database
            mBigCitiesRepository.UpdateWeatherInfo(weatherCity);

            // Return success
            return new OperationResult(true);
        }

        /// <summary>
        /// Checks if the specified date is not outdated yet
        /// </summary>
        /// <param name="date">The date to check</param>
        /// <returns>True or false</returns>
        public bool CheckWeatherDate(DateTime date)
        {
            // Get how much old is weather
            var timeDiffer = DateTime.Now - date;

            // If its older than 6h, update
            if (timeDiffer > GetOutDatedValue())
                return false;

            // Otherwise, no update
            return true;
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends an API request to find out if city exists and saves it in our database
        /// </summary>
        /// <param name="city">The name of a city to look up for</param>
        private void SendApiRequest(string city)
        {
            // Send API request and catch the response
            var response = mOpenCageGeocoderService.GetAPIInfo(city);

            // If we didn't get a city
            if (!response.Success || response.Result == null)
                // City doesn't exist
                return;

            // Otherwise, get the associated big city
            mBigCitiesRepository.AttachNewSmallCity(response.Result.Name, response.Result.Latitude, response.Result.Longitude);
        }

        /// <summary>
        /// Gets the Out-dated weather value from configuration
        /// </summary>
        /// <returns>TimeSpan</returns>
        private TimeSpan GetOutDatedValue() => mConfiguration.GetSection("WeatherConfiguration").GetValue<TimeSpan>("OutDatedTime");

        #endregion
    }
}
