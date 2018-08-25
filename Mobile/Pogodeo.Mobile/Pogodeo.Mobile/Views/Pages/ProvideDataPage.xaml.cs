using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    public partial class ProvideDataPage : ContentPage
	{
		public ProvideDataPage()
		{
			InitializeComponent();

            BindingContext = new ProvideDataViewModel();
		}
	}
}
