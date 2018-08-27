namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for show weather page
    /// </summary>
    public class ShowWeatherViewModel
    {
        #region Public Properties

        /// <summary>
        /// The name of a city that came from user
        /// </summary>
        public string CityName { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowWeatherViewModel(string city)
        {
            // Get name from previous page
            CityName = city;
        }

        #endregion
    }
}
