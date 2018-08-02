using Microsoft.AspNetCore.Mvc;

namespace Pogodeo.Controllers
{
    /// <summary>
    /// The controller for page that shows weather info to the user
    /// </summary>
    public class ShowWeatherController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
