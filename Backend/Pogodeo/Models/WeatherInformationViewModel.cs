namespace Pogodeo
{
    /// <summary>
    /// The view model for single information box from external API
    /// </summary>
    public class WeatherInformationViewModel
    {
        /// <summary>
        /// The name of the API that provided information
        /// </summary>
        public string WeatherProviderAPIName { get; set; }

        /// <summary>
        /// TODO: Make something different about weather
        /// </summary>
        public int Celsius { get; set; }
    }
}
