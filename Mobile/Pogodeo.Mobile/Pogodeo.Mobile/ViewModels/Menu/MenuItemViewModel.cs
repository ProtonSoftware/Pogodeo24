using System;
using System.Windows.Input;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for single menu item
    /// </summary>
    public class MenuItemViewModel
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
        /// <param name="action">The required action to run whenever this item is selected</param>
        public MenuItemViewModel(Action action)
        {
            // Create commands
            MenuItemSelectCommand = new RelayCommand(action);
        }

        #endregion
    }
}
