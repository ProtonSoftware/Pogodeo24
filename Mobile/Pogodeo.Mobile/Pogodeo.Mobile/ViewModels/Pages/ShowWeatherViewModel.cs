using Pogodeo.Core.Localization;
using System.Windows.Input;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for show weather page
    /// </summary>
    public class ShowWeatherViewModel : BaseViewModel
    {
        #region Private Members

        /// <summary>
        /// The raw slider value as double number
        /// </summary>
        private double mSliderValue;

        /// <summary>
        /// Indicates if switch is toggled
        /// </summary>
        private bool mIsSwitchToggled;

        #endregion

        #region Public Properties

        /// <summary>
        /// The name of a city that user selected
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// Indicates whether city was provided for this page to load
        /// </summary>
        public bool CityProvided => CityName != null;

        #endregion

        #region Commands
        
        /// <summary>
        /// The command to show a page where user can add new place
        /// </summary>
        public ICommand AddNewPlaceCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        /// <param name="city">The name of a city</param>
        public ShowWeatherViewModel(string city)
        {
            // Create commands
            AddNewPlaceCommand = new RelayCommand(async () => await DI.UI.ShowModalOnCurrentNavigation(new ProvideDataPage()));

            // Check if city's name was provided
            CityName = city;

            // Set page's title
            Title = string.Format(LocalizationResources.ShowWeatherTitle, city);
        }

        #endregion
    }
}
