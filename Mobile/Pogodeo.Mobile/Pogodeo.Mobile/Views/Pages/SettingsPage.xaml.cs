
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SettingsPage : BasePage
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsPage()
        {
            // Do default things
            InitializeComponent();

            // Set brand-new view model
            BindingContext = new AboutViewModel();
        }

        /// <summary>
        /// Constructor with specified view model to setup for this page
        /// </summary>
        public SettingsPage(SettingsViewModel viewModel)
        {
            // Do default things
            InitializeComponent();

            // Set specified view model
            BindingContext = viewModel ?? new SettingsViewModel();
        }

        #endregion
    }
}