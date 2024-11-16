using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Sử dụng cho Session
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class ServiceDetailsController : Controller
    {

        [HttpGet]
        public IActionResult Airway()
        {
            return View();
        }


        [HttpGet]
        public IActionResult Sea()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Road()
        {
            // trả về 1 giao diện trong foder views , trong views có 1 foder có tên là tiền tố của controller (ServiceDetails)
            // , trong ServiceDetails thì lại có 1 file cshtml với tên là tên của hàm đang gọi luôn Road...
            return View();
        }

    }
}
