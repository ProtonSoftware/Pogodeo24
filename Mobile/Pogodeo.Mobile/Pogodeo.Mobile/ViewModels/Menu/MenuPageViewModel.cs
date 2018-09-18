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

            // Initialize the list with fields
            CityItems = new List<MenuPlaceItemViewModel>
            {
                new MenuPlaceItemViewModel { City = "Warszawa" },
                new MenuPlaceItemViewModel { City = "Kraków" },
                new MenuPlaceItemViewModel { City = "Przemyśl" }
            };

            ApplicationItems = new List<MenuPageItemViewModel>
            {
                new MenuPageItemViewModel { Page = ApplicationPage.ProvideData, Title = "Wesprzyj nas", Icon = ApplicationIconType.Cash },
                new MenuPageItemViewModel { Page = ApplicationPage.ProvideData, Title = "Jak to dziala?", Icon = ApplicationIconType.Help },
                new MenuPageItemViewModel { Page = ApplicationPage.Settings, Title = "Ustawienia", Icon = ApplicationIconType.Settings },
                new MenuPageItemViewModel { Page = ApplicationPage.About, Title = "O nas", Icon = ApplicationIconType.About  }
            };
        }

        #endregion
    }
}
