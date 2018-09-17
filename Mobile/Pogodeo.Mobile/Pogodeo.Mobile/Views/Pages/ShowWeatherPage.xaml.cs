using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowWeatherPage : BasePage
	{
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowWeatherPage()
        {
            // Do default things
            InitializeComponent();

            // Set specified view model
            BindingContext = new ShowWeatherViewModel(null);
        }

        /// <summary>
        /// Constructor with additional view model to setup at the start
        /// </summary>
        /// <param name="viewModel">The view model with data for this page</param>
        public ShowWeatherPage(ShowWeatherViewModel viewModel)
		{
            // Do default things
			InitializeComponent();

            // Set specified view model
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
