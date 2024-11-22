using KoiDeliveryOrderingSystem.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{

    public class HomeController : Controller
    {
        public IActionResult ArticleDetails(string id, string title, string content, string imageUrl)
        {
            // Gửi dữ liệu sang View qua ViewBag
            ViewBag.Id = id;
            ViewBag.Title = title;
            ViewBag.Content = content;
            ViewBag.ImageUrl = imageUrl;

            return View();
        }
        public IActionResult Contact()
        {
            return View();  // Đảm bảo có trả về một View "Contact"
        }
        public IActionResult Support()
        {
            return View();  // Đảm bảo có trả về một View "Support"
        }
        public IActionResult FAQ()
        {
            return View();
        }
        public IActionResult About ()
        {
            return View();
        }
        public IActionResult WarrantyPolicy()
        {
            return View();
        }

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

        public IActionResult TermsAndCondition()
        {
            return View();
        }

    }
}