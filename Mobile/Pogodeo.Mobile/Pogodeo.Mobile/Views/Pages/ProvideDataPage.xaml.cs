using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvideDataPage : BasePage
	{
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ProvideDataPage()
        {
            // Do default things
            InitializeComponent();

            // Set brand-new view model
            BindingContext = new ProvideDataViewModel();
        }

        /// <summary>
        /// Constructor with additional view model to setup for this page
        /// </summary>
        public ProvideDataPage(ProvideDataViewModel viewModel)
		{
            // Do default things
			InitializeComponent();

            // Set specified view model
            BindingContext = viewModel ?? new ProvideDataViewModel();
		}

        #endregion
    }
}
