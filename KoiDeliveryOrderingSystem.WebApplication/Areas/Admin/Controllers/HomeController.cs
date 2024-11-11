using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult QuanLyDonVanChuyen()
        {
            return View();
        }


    }
}
