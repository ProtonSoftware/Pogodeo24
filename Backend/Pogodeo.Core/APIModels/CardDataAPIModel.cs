namespace Pogodeo.Core
{
    /// <summary>
    /// The model with data for the weather card
    /// Should be associated with date
    /// </summary>
    public class CardDataAPIModel
    {
        #region Public Properties

        /// <summary>
        /// The value of temperature to update to
        /// </summary>
        public int ValueTemperature { get; set; }

        /// <summary>
        /// The value of rain to update to
        /// </summary>
        public int ValueRain { get; set; }

        /// <summary>
        /// The value of humidity to update to
        /// </summary>
        public int ValueHumidity { get; set; }

        /// <summary>
        /// The value of wind speed to update to
        /// </summary>
        public int ValueWind { get; set; }

        /// <summary>
        /// The new icon to update
        /// </summary>
        public WeatherIconType WeatherIcon { get; set; }

        #endregion
    }
}
