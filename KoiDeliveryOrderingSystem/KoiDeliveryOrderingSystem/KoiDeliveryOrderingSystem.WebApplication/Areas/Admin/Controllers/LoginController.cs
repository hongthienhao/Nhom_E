using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class LoginController : Controller
    {
        // GET: Admin/Login
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string username, string password)
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                // Logic xử lý đăng nhập
                if (username == "admin" && password == "password") // Đây chỉ là ví dụ, bạn nên dùng cơ chế an toàn hơn.
                {
                    // Đăng nhập thành công
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    // Đăng nhập thất bại
                    ViewBag.ErrorMessage = "Invalid username or password";
                }
            }
            return View();
        }
    }
}
