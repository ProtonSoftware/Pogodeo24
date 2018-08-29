using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MenuPage : ContentPage
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public MenuPage()
        {
            // Do default things
            InitializeComponent();

            // Set binding to our view model
            BindingContext = new MenuPageViewModel();
        }

        #endregion
    }
}