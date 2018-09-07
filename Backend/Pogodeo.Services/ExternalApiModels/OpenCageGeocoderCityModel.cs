namespace Pogodeo.Services
{
    /// <summary>
    /// The model that OpenCageGeocoder API returns
    /// </summary>
    public class OpenCageGeocoderCityModel
    {
        #region Public Properties

        /// <summary>
        /// The name of a city
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The geographical latitude of city
        /// </summary>
        public string Latitude { get; set; }

        /// <summary>
        /// The geographical longitude of city
        /// </summary>
        public string Longitude { get; set; }

        #endregion
    }
}
