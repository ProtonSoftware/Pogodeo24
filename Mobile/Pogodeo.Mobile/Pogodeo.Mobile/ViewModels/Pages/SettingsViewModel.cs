using Pogodeo.Core.Localization;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for settings page
    /// </summary>
    public class SettingsViewModel : BaseViewModel
    {
        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public SettingsViewModel()
        {
            Title = LocalizationResources.Settings;
        }

        #endregion
    }
}