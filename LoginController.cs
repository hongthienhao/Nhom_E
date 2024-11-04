using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.DeliveringStaff.Controllers
{
    [Area("DeliveringStaff")]
    public class LoginController : Controller
    {
        // Hiển thị giao diện đăng nhập
        public IActionResult Index()
        {
            return View();
        }

        // Xử lý logic đăng nhập
        [HttpPost]
        public IActionResult Index(string email, string password)
        {
            // Tạm thời dùng thông tin tài khoản mẫu để kiểm tra
            if (email == "demo@koi.com" && password == "123456")
            {
                // Chuyển hướng đến trang chủ sau khi đăng nhập thành công
                return RedirectToAction("Index", "Home");
            }

            // Nếu đăng nhập không thành công, hiển thị lỗi
            ModelState.AddModelError("", "Email hoặc mật khẩu không đúng.");
            return View();
        }

        // Hàm đăng xuất
        public IActionResult Logout()
        {
            // Logic đăng xuất tại đây (xóa session, cookies nếu có)
            return RedirectToAction("Index");
        }
    }
}
