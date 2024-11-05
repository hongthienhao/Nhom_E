using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories; // Namespace cho HTQLKoiContext và User
using System;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly HTQLKoiContext _context;

    public AccountController(HTQLKoiContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(string email, string password)
    {
        Console.WriteLine($"Email: {email}");
        Console.WriteLine($"Password: {password}");

        // Kiểm tra thông tin đăng nhập và trạng thái hoạt động của tài khoản
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == password && u.IsActive == true);

        if (user != null)
        {
            // Lưu user_id vào session
            HttpContext.Session.SetInt32("UserId", user.UserId);

            // Điều hướng đến trang chủ hoặc trang đặt hàng
            return RedirectToAction("Index", "Home");
        }

        // Đăng nhập không thành công
        TempData["LoginError"] = "Email hoặc mật khẩu không đúng hoặc tài khoản đã bị vô hiệu hóa.";
        return View();
    }

    [HttpGet]
    public IActionResult SignUp()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> SignUp(string username, string email, string password, string confirmPassword)
    {
        if (password != confirmPassword)
        {
            TempData["SignUpError"] = "Mật khẩu không khớp.";
            return View();
        }

        // Kiểm tra xem email đã tồn tại chưa
        if (await _context.Users.AnyAsync(u => u.Email == email))
        {
            TempData["SignUpError"] = "Email này đã được đăng ký.";
            return View();
        }

        // Tạo người dùng mới
        var user = new User
        {
            Name = username,
            Email = email,
            Password = password, // Lưu mật khẩu không mã hóa (chỉ làm cho mục đích minh họa)
            CreatedAt = DateTime.Now,
            UpdatedAt = DateTime.Now,
            IsActive = true // Đặt trạng thái hoạt động mặc định là true
        };

        try
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error saving user to database: " + ex.Message);
            TempData["SignUpError"] = "Có lỗi xảy ra khi đăng ký tài khoản.";
            return View();
        }

        // Sau khi đăng ký thành công, chuyển hướng đến trang đăng nhập
        return RedirectToAction("Login", "Account");
    }

    [HttpGet]
    public IActionResult Logout()
    {
        // Xóa session khi đăng xuất
        HttpContext.Session.Remove("UserId");
        return RedirectToAction("Login");
    }
}
