using Pogodeo.Core.Localization;
using System.Collections.Generic;
using System.Windows.Input;
using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for side menu page
    /// </summary>
    public class MenuPageViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// List of weather places menu items in this page - saved user's cities
        /// </summary>
        public List<MenuPlaceItemViewModel> CityItems { get; set; }

        /// <summary>
        /// List of application menu items in this page such as settings/about etc.
        /// </summary>
        public List<MenuPageItemViewModel> ApplicationItems { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to show a page where user can add new place
        /// </summary>
        public ICommand AddNewPlaceCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default contructor
        /// </summary>
        public MenuPageViewModel()
        {
            // Create commands
            AddNewPlaceCommand = new RelayCommand(async () => await DI.UI.ShowModalOnCurrentNavigation(new ProvideDataPage()));

            // Initialize the list with city fields
            InitializeCityNamesFields();

            // TODO: Maybe something different here
            ApplicationItems = new List<MenuPageItemViewModel>
            {
                new MenuPageItemViewModel { Page = ApplicationPage.ProvideData, Title = LocalizationResources.HelpUs, Icon = ApplicationIconType.Cash },
                new MenuPageItemViewModel { Page = ApplicationPage.ProvideData, Title = LocalizationResources.HowItWorks, Icon = ApplicationIconType.Help },
                new MenuPageItemViewModel { Page = ApplicationPage.Settings, Title = LocalizationResources.Settings, Icon = ApplicationIconType.Settings },
                new MenuPageItemViewModel { Page = ApplicationPage.About, Title = LocalizationResources.About, Icon = ApplicationIconType.About  }
            };
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Initializes the city names fields for menu list by requesting them from database
        /// </summary>
        private void InitializeCityNamesFields()
        {
            // Get every saved city name from database
            var cityNames = DI.CityWeatherRepository.GetListOfSavedCities();

            // Initialize the list
            CityItems = new List<MenuPlaceItemViewModel>();

            // For every city name...
            foreach (var name in cityNames)
                // Add new menu item
                CityItems.Add(new MenuPlaceItemViewModel(name));
        }

        #endregion
    }
}
