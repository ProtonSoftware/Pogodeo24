using Dna;
using Microsoft.AspNetCore.Mvc;
using Pogodeo.Core;
using System.Threading.Tasks;

namespace Pogodeo
{
    /// <summary>
    /// The controller for page that shows weather info to the user
    /// </summary>
    public class ShowWeatherController : Controller
    {
        public async Task<IActionResult> Index(ProvideDataViewModel viewModel)
        {
            // Get data from our API
            var apiData = new ShowWeatherViewModel();
            
            // Send a request
            var apiResult = await WebRequests.PostAsync<APIWeatherResponse>(GetAPIRoute(), viewModel.CityName);
                
            // If we got a data...
            if (apiResult.Successful)
            {
                // Deserialize json to suitable view model
                apiData.CityName = viewModel.CityName;
                apiData.APIResponse = apiResult.ServerResponse;
            }

            // Show the page to the user
            return View(apiData);
        }

        #region Private Helpers

        /// <summary>
        /// Gets the url for API call
        /// </summary>
        /// <returns>URL</returns>
        private string GetAPIRoute() => Request.Scheme + "://" + Request.Host + ApiRoutes.GetWeatherForCity;

        #endregion
    }
}
