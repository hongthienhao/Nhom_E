using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class DeliveryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
