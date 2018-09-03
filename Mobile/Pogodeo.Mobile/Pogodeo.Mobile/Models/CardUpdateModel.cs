namespace Pogodeo.Mobile
{
    /// <summary>
    /// The model with new data for the card to update
    /// </summary>
    public class CardUpdateModel
    {
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
    }
}
