using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Pogodeo.Core;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The repository for database to handle cities' weather table
    /// </summary>
    public class CityWeatherRepository : BaseRepository<CityWeather, int>, ICityWeatherRepository
    {
        #region Protected Properties

        /// <summary>
        /// Interface implementation to access the cities' weather table
        /// </summary>
        protected override DbSet<CityWeather> DbSet => Db.CityWeather;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">Application's database</param>
        public CityWeatherRepository(PogodeoMobileDbContext db) : base(db)
        {
        }

        #endregion

        #region Public Methods

        /// <summary>
        /// Saves weather informations to the database for specified city
        /// </summary>
        /// <param name="city">The city's name</param>
        /// <param name="weather">The weather that is associated with this city</param>
        public void SaveWeatherForCity(string city, Dictionary<APIProviderType, WeatherInformationAPIModel> weather)
        {
            // Try to get the entity for this city, if none was found - create new one
            var entity = GetByName(city) ?? new CityWeather
            {
                CityName = city
            };

            // Save weather data as json string
            entity.WeatherData = JsonConvert.SerializeObject(weather);

            // Save current date as upload date
            entity.LastUpdateDate = DateTime.Now;

            // Add it to the database
            DbSet.Add(entity);

            // Save it
            SaveChanges();
        }

        /// <summary>
        /// Gets weather informations from the database for specified city
        /// </summary>
        /// <param name="city">The city's name</param>
        public CityWeatherContext GetCityWeather(string city)
        {
            // Try to get the entity for this city
            var entity = GetByName(city);

            // If none was found
            if (entity == null)
                // Return empty weather
                return null;

            // Otherwise, return mapped entity
            return DI.CityMapper.Map(entity);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Gets the entity by city's name
        /// </summary>
        public CityWeather GetByName(string city)
        {
            // Try to find the entity
            var result = DbSet.Where(x => InsensitiveStringComparition(x.CityName, city)).FirstOrDefault();

            // Return what was found
            return result;
        }

        #endregion
    }
}
