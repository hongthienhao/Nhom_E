using KoiDeliveryOrderingSystem.WebApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public IActionResult PricingPlan()
        {
            return View();
        }

        public IActionResult Features()
        {
            return View();
        }

        public IActionResult FreeQuote()
        {
            return View();
        }

        public IActionResult OurTeam()
        {
            return View();
        }

        public IActionResult Testimonial()
        {
            return View();
        }

        public IActionResult PageNotFound()
        {
            return View("404"); // Đảm bảo bạn có View 404.cshtml trong thư mục Views/Home
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
