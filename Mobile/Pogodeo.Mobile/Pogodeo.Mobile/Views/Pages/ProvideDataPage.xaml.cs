using Xamarin.Forms.Xaml;

namespace Pogodeo.Mobile
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProvideDataPage : BasePage
	{
		public ProvideDataPage()
		{
			InitializeComponent();

            BindingContext = new ProvideDataViewModel();
		}
	}
}
