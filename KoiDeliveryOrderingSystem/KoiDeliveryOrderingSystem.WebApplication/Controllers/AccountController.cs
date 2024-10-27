using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.WebApplication.Data;
using KoiDeliveryOrderingSystem.WebApplication.Models;
using System.Security.Cryptography;
using System.Text;

public class AccountController : Controller
{
    private readonly HtqlkoiContext _context;

    public AccountController(HtqlkoiContext context)
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
        var hashedPassword = HashPassword(password);
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email && u.Password == hashedPassword);

        if (user != null)
        {
            HttpContext.Session.SetInt32("UserId", user.UserId);
            return RedirectToAction("Index", "Home");
        }

        TempData["LoginError"] = "Email hoặc mật khẩu không đúng.";
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

        if (await _context.Users.AnyAsync(u => u.Email == email))
        {
            TempData["SignUpError"] = "Email này đã được đăng ký.";
            return View();
        }

        var user = new User
        {
            Name = username,
            Email = email,
            Password = HashPassword(password),
            Role = "Customer" // Default role
        };

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("Login", "Account");
    }

    private string HashPassword(string password)
    {
        using (var sha256 = SHA256.Create())
        {
            byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            StringBuilder builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
