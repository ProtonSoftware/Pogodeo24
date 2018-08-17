using Microsoft.AspNetCore.Mvc;

namespace Pogodeo
{
    /// <summary>
    /// The controller for page that shows weather info to the user
    /// </summary>
    public class ShowWeatherController : Controller
    {
        public IActionResult Index(ProvideDataViewModel viewModel)
        {
            return View();
        }
    }
}
