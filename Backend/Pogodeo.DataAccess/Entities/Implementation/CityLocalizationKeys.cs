namespace Pogodeo.DataAccess
{
    /// <summary>
    /// The entity for city localization keys table in database
    /// </summary>
    public class CityLocalizationKeys : BaseObject<int>
    {
        #region Public Properties
        
        /// <summary>
        /// Name of the city
        /// Should be unique
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Localization key for AccuWeather API 
        /// </summary>
        public string AccuWeatherLocalizationKey { get; set; }
        
        #endregion
    }
}
