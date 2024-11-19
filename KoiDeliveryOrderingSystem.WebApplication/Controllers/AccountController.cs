using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http; // Sử dụng cho Session
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            try
            {
                var user = await _accountService.LoginAsync(email, password);

                // Lưu UserId vào session
                HttpContext.Session.SetInt32("UserId", user.UserId);

                // Kiểm tra vai trò thông qua RoleId
                switch (user.RoleId)
                {
                    case 1:
                        // Nếu là admin (RoleId = 1)
                        return RedirectToAction("Index", "Home", new { area = "Admin" });

                    case 2:
                        // Nếu là DeliveringStaff (RoleId = 2)
                        return Redirect("https://localhost:7040/DeliveringStaff/Home/Index");

                    case 3:
                        // Nếu là SalesStaff (RoleId = 3)
                        return Redirect("https://localhost:7040/SalesStaff/Home/Index");

                    default:
                        // Nếu không phải các role trên, điều hướng đến trang Home
                        return RedirectToAction("Index", "Home");
                }
            }
            catch (Exception ex)
            {
                // Gửi thông báo lỗi nếu đăng nhập không thành công
                TempData["LoginError"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult SignUp()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignUp(string username, string email, string password, string confirmPassword)
        {
            try
            {
                // Gọi service để xử lý đăng ký
                await _accountService.RegisterAsync(username, email, password, confirmPassword);

                // Sau khi đăng ký thành công, điều hướng đến trang đăng nhập
                return RedirectToAction("Login");
            }
            catch (Exception ex)
            {
                // Gửi thông báo lỗi nếu đăng ký thất bại
                TempData["SignUpError"] = ex.Message;
                return View();
            }
        }

        [HttpGet]
        public IActionResult Logout()
        {
            // Xóa session khi đăng xuất
            HttpContext.Session.Remove("UserId");
            return RedirectToAction("Login");
        }
    }
}
