using Dna;
using Pogodeo.Core;
using System.Threading.Tasks;

namespace Pogodeo.Mobile
{
    /// <summary>
    /// The view model for show weather page
    /// </summary>
    public class ShowWeatherViewModel : BaseViewModel
    {
        #region Public Properties

        /// <summary>
        /// The name of a city that came from user
        /// </summary>
        public string CityName { get; set; }

        /// <summary>
        /// The response from API that should be shown in this page
        /// </summary>
        public APIWeatherResponse APIResponse { get; set; }

        #endregion

        #region Constructor

        /// <summary>
        /// Default constructor
        /// </summary>
        public ShowWeatherViewModel(string city)
        {
            // Set page's title
            Title = "Show weather";

            // Get name from previous page
            CityName = city;

            // Send an API request
            Task.Run(GetAPIData);
        }

        #endregion

        #region Private Helpers

        /// <summary>
        /// Sends an API request and collects the data
        /// </summary>
        private async Task GetAPIData()
        {
            // Send an API request
            var apiResult = await WebRequests.PostAsync<APIWeatherResponse>(GetAPIRoute(), CityName);

            // If we got a data back...
            if (apiResult != null && apiResult.Successful && apiResult.ServerResponse != null)
                // Deserialize json to suitable view model
                APIResponse = apiResult.ServerResponse;
        }

        /// <summary>
        /// Gets the url for API call
        /// </summary>
        /// <returns>URL</returns>
        private string GetAPIRoute() => "http://pogodeo24.pl" + ApiRoutes.GetWeatherForCity;

        #endregion
    }
}
