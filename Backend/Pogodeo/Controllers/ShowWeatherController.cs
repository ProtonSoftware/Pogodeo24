using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace Pogodeo
{
    /// <summary>
    /// The controller for page that shows weather info to the user
    /// </summary>
    public class ShowWeatherController : Controller
    {
        public IActionResult Index(ProvideDataViewModel viewModel)
        {
            // TODO: Get real data from APIs
            var apiData = new ShowWeatherViewModel
            {
                CityName = viewModel.CityName,
                WeatherInformationsList = new List<WeatherInformationViewModel>
                {
                    new WeatherInformationViewModel
                    {
                        WeatherProviderAPIName = "Onet",
                        Celsius = 20
                    },
                    new WeatherInformationViewModel
                    {
                        WeatherProviderAPIName = "WP",
                        Celsius = 21
                    }
                }
            };

            // Show the page to the user
            return View(apiData);
        }
    }
}
