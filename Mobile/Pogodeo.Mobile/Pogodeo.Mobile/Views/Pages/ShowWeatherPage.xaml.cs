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
        /// <param name="viewModel">The required view model with data for this page</param>
        public ShowWeatherPage(ShowWeatherViewModel viewModel)
		{
            // Do default things
			InitializeComponent();

            // Set specified view model
            BindingContext = viewModel;
		}

        #endregion
    }
}
