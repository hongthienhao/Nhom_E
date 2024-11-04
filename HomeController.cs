using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.DeliveringStaff.Controllers
{
    [Area("DeliveringStaff")]
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult OrderDetails()
        {
            return View();
        }

        public IActionResult UpdateStatus()
        {
            return View();
        }
    }
}
