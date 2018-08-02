using Microsoft.AspNetCore.Mvc;

namespace Pogodeo.Controllers
{
    /// <summary>
    /// The controller for initial page that accepts data from user
    /// </summary>
    public class ProvideDataController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
