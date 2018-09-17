using System.Windows.Input;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for single menu item that leads to the specified page
    /// </summary>
    public class MenuPageItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The page that this item is associated with
        /// </summary>
        public ApplicationPage Page { get; set; }

        /// <summary>
        /// The title of this item
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// The icon of this item
        /// </summary>
        public ApplicationIconType Icon { get; set; }

        #endregion

        #region Commands

        /// <summary>
        /// The command to fire when this item is selected
        /// </summary>
        public ICommand MenuItemSelectCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuPageItemViewModel()
        {
            // Create commands
            MenuItemSelectCommand = new RelayCommand(ChangePage);
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Fired when this item is clicked
        /// Changes the page to the one that is associated with this item
        /// </summary>
        private void ChangePage()
        {
            // Simply change the page
            DI.Application.GoToPage(Page);
        }

        #endregion
    }
}
