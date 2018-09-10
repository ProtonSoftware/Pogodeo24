namespace Pogodeo.Core
{
    /// <summary>
    /// The model with data for the days weather card
    /// Should be associated with date
    /// </summary>
    public class CardDayDataAPIModel
    {
        #region Public Properties

        /// <summary>
        /// The day value of temperature in current date
        /// </summary>
        public int? DayTemperature { get; set; }

        /// <summary>
        /// The night value of temperature in current date
        /// </summary>
        public int? NightTemperature { get; set; }

        /// <summary>
        /// The icon for day in current date
        /// </summary>
        public WeatherIconType DayWeatherIcon { get; set; }

        /// <summary>
        /// The icon for night in current date
        /// </summary>
        public WeatherIconType NightWeatherIcon { get; set; }

        #endregion
    }
}
