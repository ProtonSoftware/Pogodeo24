using System;

namespace Pogodeo.DataAccess
{
    /// <summary>
    /// The entity for big city weather
    /// </summary>
    public class BigCityWeather : BaseObject<int>
    {
        #region Public Properties

        /// <summary>
        /// The last date this entry was updated
        /// </summary>
        public DateTime LastUpdateDate { get; set; }

        #endregion

        #region Relational Properties

        /// <summary>
        /// The city that this weather is associated with
        /// </summary>
        public virtual BigCity AssociatedBigCity { get; set; }

        #endregion
    }
}
