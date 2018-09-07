namespace Pogodeo.DataAccess
{
    /// <summary>
    /// The entity for small city
    /// </summary>
    public class SmallCity : BaseObject<int>
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

        #endregion

        #region Relational Properties

        /// <summary>
        /// Associated big city with that one
        /// Used to get weather
        /// </summary>
        public virtual BigCity AssociatedBigCity { get; set; }

        #endregion
    }
}
