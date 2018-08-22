using Microsoft.AspNetCore.Mvc;
using Pogodeo.DataAccess;
using Pogodeo.Services;

namespace Pogodeo
{
    /// <summary>
    /// The controller for initial page that accepts data from user
    /// </summary>
    public class ProvideDataController : Controller
    {
        private readonly ITestService _service;

        public ProvideDataController(ITestService service, PogodeoAppDataContext data)
        {
            data.Database.EnsureCreated();
            _service = service;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
