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

            // Create view model or city weather control
            var weatherViewModel = new CityWeatherMainViewModel(viewModel.CityName);

            // Create control to host weather informations
            var control = new CityWeatherMainControl(weatherViewModel);

            // Add this control to the container
            WeatherContainer.Children.Add(control);
        }

        #endregion
    }
}
