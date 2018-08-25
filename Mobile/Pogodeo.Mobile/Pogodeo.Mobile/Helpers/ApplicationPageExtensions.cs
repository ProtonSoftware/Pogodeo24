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
        /// <returns>The page as actual view</returns>
        public static Page ToApplicationPage(this ApplicationPage page)
        {
            // Based on provided page...
            switch (page)
            {
                case ApplicationPage.ProvideData:
                    return new ProvideDataPage();

                case ApplicationPage.ShowWeather:
                    return new ShowWeatherPage();

                // If no page was found, return initial one
                default:
                    return new ProvideDataPage();
            }
        }
    }
}
