using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pogodeo.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// The repository that gets us localization informations about big saved cities from the database
    /// </summary>
    public class BigCitiesRepository : BaseRepository<BigCity, int>, IBigCitiesRepository
    {
        #region Private Members

        /// <summary>
        /// The AutoMapper for city objects
        /// </summary>
        private readonly CityMapper mCityMapper;

        #endregion

        #region Protected Properties

        /// <summary>
        /// Interface implementation to access the big cities table
        /// </summary>
        protected override DbSet<BigCity> DbSet => Db.BigCitiesData;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">This app's database</param>
        public BigCitiesRepository(PogodeoAppDataContext db, CityMapper cityMapper) : base(db)
        {
            mCityMapper = cityMapper;
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Looks for city with specified name
        /// </summary>
        /// <param name="city">The name of a city to look up for</param>
        /// <returns><see cref="BigCityContext"/></returns>
        public BigCityContext GetByName(string city)
        {
            // Get the entity from database
            var result = DbSet.Where(model => InsensitiveStringComparition(model.CityName, city)).Include(x => x.AccuWeatherWeather).Include(x => x.AerisWeatherWeather).Include(x => x.WWOWeather).FirstOrDefault();

            // If we didn't get one
            if (result == null)
                return null;

            // Otherwise, map it as context and return
            return mCityMapper.Map(result);
        }

        /// <summary>
        /// Creates new small city entity and attaches it to the big city
        /// </summary>
        /// <param name="name">The name of new city</param>
        /// <param name="latitude">The langitude of new city</param>
        /// <param name="longitude">The longitude of new city</param>
        public void AttachNewSmallCity(string name, string latitude, string longitude)
        {
            // Create new small city entity
            var smallCity = new SmallCity
            {
                CityName = name,
                Latitude = latitude,
                Longitude = longitude
            };

            // Find nearest big city
            var nearestBigCity = FindNearestBigCity(latitude, longitude);

            // Attach it to the small city
            smallCity.AssociatedBigCity = nearestBigCity;

            // Add small city to the big city list
            if (nearestBigCity.SubordinateCities == null)
                nearestBigCity.SubordinateCities = new List<SmallCity>();
            nearestBigCity.SubordinateCities.Add(smallCity);

            // Save it to the database
            SaveChanges();
        }

        /// <summary>
        /// Gets the localization key for AccuWeather API based on specified city
        /// </summary>
        /// <param name="city">The city to look up for</param>
        /// <returns>Localization key as string</returns>
        public string GetAccuWeatherLocalizationKey(string city)
        {
            // Get the entity from database
            var result = DbSet.Where(model => InsensitiveStringComparition(model.CityName, city)).FirstOrDefault();

            // Return localization key only if we found one
            return result?.AccuWeatherLocalizationKey;
        }

        /// <summary>
        /// Updates the localization key for specified city
        /// </summary>
        /// <param name="city">The city of which localization key is being updated</param>
        /// <param name="localizationKey">Localization key to update</param>
        public void UpdateAccuWeatherLocalizationKey(string city, string localizationKey)
        {
            // Get the entity from database
            var result = DbSet.Where(model => InsensitiveStringComparition(model.CityName, city)).FirstOrDefault();

            // If we didn't get one
            if (result == null)
                // Don't update anything
                return;

            // Update its localization key
            result.AccuWeatherLocalizationKey = localizationKey;

            // Update it in the database by saving changes
            SaveChanges();
        }

        /// <summary>
        /// Updates the AerisWeather weather info for specified city
        /// </summary>
        /// <param name="context">The city of which weather is being updated</param>
        public void UpdateWeatherInfo(BigCityContext context)
        {
            // Get the entity from database
            var result = DbSet.Where(model => InsensitiveStringComparition(model.CityName, context.CityName)).FirstOrDefault();

            // If we didn't get one
            if (result == null)
                // Don't update anything
                return;

            // If none AccuWeather weather exists for this city
            if (result.AccuWeatherWeather == null)
                // Create new one
                result.AccuWeatherWeather = new AccuWeather();

            // If none AerisWeather weather exists for this city
            if (result.AerisWeatherWeather == null)
                // Create new one
                result.AerisWeatherWeather = new AerisWeather();

            // If none WWO weather exists for this city
            if (result.WWOWeather == null)
                // Create new one
                result.WWOWeather = new WWO();

            // Update AccuWeather weather info
            result.AccuWeatherWeather.WeatherHourData = JsonConvert.SerializeObject(context.AccuWeatherContext.TodayWeatherTruncatedData);
            result.AccuWeatherWeather.WeatherDayData = JsonConvert.SerializeObject(context.AccuWeatherContext.NextDaysWeatherTruncatedData);
            result.AccuWeatherWeather.LastUpdateDate = DateTime.Now;

            // Update AerisWeather weather info
            result.AerisWeatherWeather.WeatherHourData = JsonConvert.SerializeObject(context.AerisWeatherContext.TodayWeatherTruncatedData);
            result.AerisWeatherWeather.WeatherDayData = JsonConvert.SerializeObject(context.AerisWeatherContext.NextDaysWeatherTruncatedData);
            result.AerisWeatherWeather.LastUpdateDate = DateTime.Now;

            // Update WWO weather info
            result.WWOWeather.WeatherHourData = JsonConvert.SerializeObject(context.WWOContext.TodayWeatherTruncatedData);
            result.WWOWeather.WeatherDayData = JsonConvert.SerializeObject(context.WWOContext.NextDaysWeatherTruncatedData);
            result.WWOWeather.LastUpdateDate = DateTime.Now;

            // Update it in the database by saving changes
            SaveChanges();
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Finds the nearest city from specified geographical data
        /// </summary>
        /// <param name="latitude">The latitude</param>
        /// <param name="longitude">The longitude</param>
        /// <returns></returns>
        private BigCity FindNearestBigCity(string latitude, string longitude)
        {
            // Prepare dictionary to save every distance
            var distanceDictionary = new Dictionary<BigCity, double>();

            // Get the list of big cities
            var bigCityList = GetAll();

            // For each one of them...
            foreach (var bigCity in bigCityList)
            {
                // Calculate the distance
                var distance = CalculateDistance(latitude, longitude, bigCity.Latitude, bigCity.Longitude);

                // Add it to the list
                distanceDictionary.Add(bigCity, distance);
            }

            // Return city with lowest distance
            return distanceDictionary.OrderBy(entry => entry.Value).First().Key;
        }

        /// <summary>
        /// Calculates the distance between two places
        /// </summary>
        private double CalculateDistance(string latitudeFirst, string longitudeFirst, string latitudeSecond, string longitudeSecond)
        {
            var lat1 = double.Parse(latitudeFirst);
            var lon1 = double.Parse(longitudeFirst);
            var lat2 = double.Parse(latitudeSecond);
            var lon2 = double.Parse(longitudeSecond);

            var earthRadiusKm = 6371;

            var dLat = DegreesToRadians(lat2 - lat1);
            var dLon = DegreesToRadians(lon2 - lon1);

            lat1 = DegreesToRadians(lat1);
            lat2 = DegreesToRadians(lat2);

            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) +
                    Math.Sin(dLon / 2) * Math.Sin(dLon / 2) * Math.Cos(lat1) * Math.Cos(lat2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

            return earthRadiusKm * c;
        }

        /// <summary>
        /// Converts degrees to radians unit
        /// </summary>
        private double DegreesToRadians(double degrees) => degrees * Math.PI / 180;

        #endregion
    }
}
