using System.Windows.Input;
using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for single menu item that is a saved place item
    /// </summary>
    public class MenuPlaceItemViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The name of city that this item leads to
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// The icon of this item
        /// Place icon by default
        /// </summary>
        public ApplicationIconType Icon { get; set; } = ApplicationIconType.Place;

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
        /// <param name="city">The name of a city for this menu item</param>
        public MenuPlaceItemViewModel(string city)
        {
            // Create commands
            MenuItemSelectCommand = new RelayCommand(ChangeCity);

            // Set the city's name
            City = city;
        }

        #endregion

        #region Command Methods

        /// <summary>
        /// Fired when this item is clicked
        /// Changes the city to the one associated with this item
        /// </summary>
        private void ChangeCity()
        {
            // Simply change the page with this city passed in
            DI.Application.GoToPage(ApplicationPage.ShowWeather, new ShowWeatherViewModel(City));
        }

        #endregion
    }
}
