namespace Pogodeo.Services
{
    /// <summary>
    /// The model that we get as a response from AccuWeather containing Localization Key
    /// </summary>
    public class AccuWeatherLocalizationKeyModel
    {
        /// <summary>
        /// The name of the city that is associated with the key
        /// </summary>
        public string LocalizedName { get; set; }

        /// <summary>
        /// The localization key for this city
        /// </summary>
        public string Key { get; set; }
    }
}
