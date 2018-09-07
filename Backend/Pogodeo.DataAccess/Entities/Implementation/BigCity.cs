using System.Collections.Generic;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// The entity for big city
    /// </summary>
    public class BigCity : BaseObject<int>
    {
        #region Public Properties
        
        /// <summary>
        /// Name of the city
        /// Should be unique
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Geographical latitude of this city
        /// Used for calculating distances
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// Geographical longitude of this city
        /// Used for calculating distances
        /// </summary>
        public string Longitude { get; set; }

        /// <summary>
        /// Localization key for AccuWeather API 
        /// </summary>
        public string AccuWeatherLocalizationKey { get; set; }

        #endregion

        #region Relational Properties

        /// <summary>
        /// List of subordinate small cities that get weather from this big one
        /// </summary>
        public virtual List<SmallCity> SubordinateCities { get; set; }

        #endregion
    }
}
