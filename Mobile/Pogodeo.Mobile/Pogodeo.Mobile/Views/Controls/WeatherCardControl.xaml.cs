
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class WeatherCardControl : ContentView
	{
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public WeatherCardControl ()
		{
            // Do default things
			InitializeComponent();
		}

        #endregion
    }
}