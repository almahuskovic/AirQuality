using Microsoft.AspNetCore.Mvc;

namespace AirQuality.Controllers
{
    public class AirQualityController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
