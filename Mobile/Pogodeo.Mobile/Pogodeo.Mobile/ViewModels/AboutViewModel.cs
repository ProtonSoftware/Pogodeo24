using System;
using System.Windows.Input;

using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    public class AboutViewModel : BaseViewModel
    {
        public AboutViewModel()
        {
            Title = "About";

            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://pogodeo24.pl")));
        }

        public ICommand OpenWebCommand { get; }
    }
}