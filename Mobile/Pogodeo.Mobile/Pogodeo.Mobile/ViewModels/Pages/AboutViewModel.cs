using System;
using System.Windows.Input;
using Pogodeo.Core.Localization;

using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for about page
    /// </summary>
    public class AboutViewModel : BaseViewModel
    {
        #region Commands

        /// <summary>
        /// The command to open app's website
        /// </summary>
        public ICommand OpenWebCommand { get; private set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public AboutViewModel()
        {
            // Create commands
            OpenWebCommand = new Command(() => Device.OpenUri(new Uri("https://pogodeo24.pl")));

            Title = LocalizationResources.About;
        }

        #endregion
    }
}