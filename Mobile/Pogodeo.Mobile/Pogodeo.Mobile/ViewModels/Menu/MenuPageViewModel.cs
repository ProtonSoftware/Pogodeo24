using System.Collections.Generic;

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
        public List<MenuItemViewModel> CityItems { get; set; }

        /// <summary>
        /// List of application menu items in this page such as settings/about etc.
        /// </summary>
        public List<MenuItemViewModel> ApplicationItems { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default contructor
        /// </summary>
        public MenuPageViewModel()
        {
            // Initialize the list with fields
            CityItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Page = ApplicationPage.ProvideData, Title = "Warszawa", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.ProvideData, Title = "Kraków", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.Settings, Title = "Przemyśl", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.About, Title = "JarekSławek", Icon = ApplicationIconType.Settings  }
            };

            ApplicationItems = new List<MenuItemViewModel>
            {
                new MenuItemViewModel { Page = ApplicationPage.ProvideData, Title = "Wesprzyj nas", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.ProvideData, Title = "Jak to dziala?", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.Settings, Title = "Ustawienia", Icon = ApplicationIconType.Settings },
                new MenuItemViewModel { Page = ApplicationPage.About, Title = "O nas", Icon = ApplicationIconType.About  }
            };
        }

        #endregion
    }
}
