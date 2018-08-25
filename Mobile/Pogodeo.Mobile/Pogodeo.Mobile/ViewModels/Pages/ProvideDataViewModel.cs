using System.Windows.Input;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for initial provide data page
    /// </summary>
    public class ProvideDataViewModel
    {
        #region Public Properties

        /// <summary>
        /// The name of a city that user provided in input
        /// </summary>
        public string CityName { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to change page to the next one and send input data
        /// </summary>
        public ICommand ChangePageWithDataCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProvideDataViewModel()
        {
            // Create commands
            ChangePageWithDataCommand = new RelayCommand(ChangePage);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Changes the page and sends the data
        /// </summary>
        private void ChangePage()
        {
            DI.Application.GoToPage(ApplicationPage.ShowWeather);
        }

        #endregion
    }
}
