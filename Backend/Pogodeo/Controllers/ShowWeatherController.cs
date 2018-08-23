using Microsoft.AspNetCore.Mvc;
using Pogodeo.Services;
using Pogodeo.Services.ExternalApiServices;
using System.Collections.Generic;

namespace Pogodeo
{
    /// <summary>
    /// The controller for page that shows weather info to the user
    /// </summary>
    public class ShowWeatherController : Controller
    {
        private readonly ITestService _service;
        private readonly IOpenCageGeocoder _geo;

        public ShowWeatherController(ITestService service, IOpenCageGeocoder geo)
        {
            _service = service;
            _geo = geo;
        }

        public IActionResult Index(ProvideDataViewModel viewModel)
        {
            var a = _geo.GetAddressLocation(viewModel.CityName);

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
