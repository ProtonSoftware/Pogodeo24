namespace Pogodeo.Services
{
    /// <summary>
    /// The context for <see cref="SmallCity"/>
    /// </summary>
    public class SmallCityContext
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
        /// Associated big city with that one
        /// Used to get weather
        /// </summary>
        public BigCityContext AssociatedBigCity { get; set; }

        #endregion
    }
}
