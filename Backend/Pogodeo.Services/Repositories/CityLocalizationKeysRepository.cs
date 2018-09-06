using Microsoft.EntityFrameworkCore;
using Pogodeo.DataAccess;
using System.Linq;

namespace Pogodeo.Services
{
    /// <summary>
    /// The repository that saves localization informations to the database
    /// </summary>
    public class CityLocalizationKeysRepository : BaseRepository<CityLocalizationKeys, int>, ICityLocalizationKeysRepository
    {
        #region Protected Properties

        /// <summary>
        /// Interface implementation to access the city localization keys table
        /// </summary>
        protected override DbSet<CityLocalizationKeys> DbSet => Db.CityLocalizationKeys;

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="db">This app's database</param>
        public CityLocalizationKeysRepository(PogodeoAppDataContext db) : base(db) { }

        #endregion

        #region Public Methods

        /// <summary>
        /// Gets the localization key for AccuWeather API based on specified city
        /// </summary>
        /// <param name="city">The city to look up for</param>
        /// <returns>Localization key as string</returns>
        public string GetAccuWeatherLocalizationKey(string city)
        {
            // Get the entity from database
            var result = DbSet.Where(model => model.CityName == city).FirstOrDefault();

            // Return localization key only if we found one
            return result == null ? null : result.AccuWeatherLocalizationKey;
        }

        #endregion
    }
}
