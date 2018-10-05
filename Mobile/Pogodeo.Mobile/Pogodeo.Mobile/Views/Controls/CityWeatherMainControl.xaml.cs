using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class CityWeatherMainControl : ContentView
	{
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public CityWeatherMainControl(CityWeatherMainViewModel viewModel)
		{
            // Do default things
			InitializeComponent();

            // Set provided view model for this control
            BindingContext = viewModel;

            // Foreach item in the view model
            foreach (var item in viewModel.Items)
            {
                // Create new card control
                var viewItem = new WeatherCardControl();

                // Set the binding for control
                viewItem.BindingContext = item;

                // Add it to the list
                ListContainer.Children.Add(viewItem);
            }
        }

        #endregion
    }
}