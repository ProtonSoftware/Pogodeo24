using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ShowWeatherPage : BasePage
	{
		public ShowWeatherPage()
		{
			InitializeComponent();

            BindingContext = new ShowWeatherViewModel("Przemysl");
		}
    }
}
