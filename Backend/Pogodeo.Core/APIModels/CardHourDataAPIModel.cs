namespace Pogodeo.Core
{
    /// <summary>
    /// The model with data for the hour-based weather card
    /// Should be associated with date
    /// </summary>
    public class CardHourDataAPIModel
    {
        #region Public Properties

        /// <summary>
        /// The value of temperature in current date
        /// </summary>
        public int? ValueTemperature { get; set; }

        /// <summary>
        /// The value of rain in current date
        /// </summary>
        public double? ValueRain { get; set; }

        /// <summary>
        /// The value of humidity in current date
        /// </summary>
        public int? ValueHumidity { get; set; }

        /// <summary>
        /// The value of wind speed in current date
        /// </summary>
        public int? ValueWind { get; set; }

        /// <summary>
        /// The icon in current dates
        /// </summary>
        public WeatherIconType WeatherIcon { get; set; }

        #endregion
    }
}
