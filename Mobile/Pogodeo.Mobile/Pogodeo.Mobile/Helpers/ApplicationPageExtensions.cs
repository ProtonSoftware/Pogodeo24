using Xamarin.Forms;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// Extensions for <see cref="ApplicationPage"/>
    /// </summary>
    public static class ApplicationPageExtensions
    {
        /// <summary>
        /// Returns actual application page based on enum
        /// </summary>
        /// <param name="page">The page as enum</param>
        /// <param name="viewModel">The view model to set initially on page (if provided)</param>
        /// <returns>The page as actual view</returns>
        public static Page ToApplicationPage(this ApplicationPage page, BaseViewModel viewModel = null)
        {
            // Based on provided page...
            switch (page)
            {
                case ApplicationPage.ProvideData:
                    return new ProvideDataPage(viewModel as ProvideDataViewModel);

                case ApplicationPage.ShowWeather:
                    return new ShowWeatherPage(viewModel as ShowWeatherViewModel);

                case ApplicationPage.About:
                    return new AboutPage(viewModel as AboutViewModel);

                // If no page was found, return initial one
                default:
                    return new ProvideDataPage(viewModel as ProvideDataViewModel);
            }
        }
    }
}
