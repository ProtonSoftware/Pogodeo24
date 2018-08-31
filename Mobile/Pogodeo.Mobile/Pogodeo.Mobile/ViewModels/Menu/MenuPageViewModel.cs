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
        /// List of menu items in this page
        /// </summary>
        public List<MenuItemViewModel> Items { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default contructor
        /// </summary>
        public MenuPageViewModel()
        {
            // Initialize the list with fields
            Items = new List<MenuItemViewModel>
            {
                new MenuItemViewModel(() => DI.Application.GoToPage(ApplicationPage.ProvideData)) { Page = ApplicationPage.ProvideData, Title = "Provide Data" },
                new MenuItemViewModel(() => DI.Application.GoToPage(ApplicationPage.About)) { Page = ApplicationPage.About, Title = "About"  }
            };
        }

        #endregion

        
    }
}
