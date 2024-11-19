using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        // Hiển thị danh sách khách hàng
        public async Task<IActionResult> Index()
        {
            var users = await _service.GetAllUsersAsync();
            return View(users);
        }

        // Hiển thị chi tiết khách hàng
        public async Task<IActionResult> Details(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            // Load lịch sử đơn hàng
            var orders = await _service.GetUserOrdersAsync(id);
            ViewBag.Orders = orders;

            return View(user);
        }

        // Hiển thị form chỉnh sửa thông tin khách hàng
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _service.GetUserByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

        // Xử lý cập nhật thông tin khách hàng
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            return View(user);
        }

        // Khóa tài khoản khách hàng
        public async Task<IActionResult> Lock(int id)
        {
            await _service.LockUserAccountAsync(id);
            return RedirectToAction("Index");
        }

        // Mở khóa tài khoản khách hàng
        public async Task<IActionResult> Unlock(int id)
        {
            await _service.UnlockUserAccountAsync(id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer([FromBody] User user)
        {
            if (user == null || user.UserId <= 0)
            {
                return Json(new { success = false, message = "Dữ liệu không hợp lệ." });
            }

            var existingUser = await _service.GetUserByIdAsync(user.UserId);
            if (existingUser == null)
            {
                return Json(new { success = false, message = "Không tìm thấy người dùng." });
            }

            // Cập nhật thông tin khách hàng
            existingUser.Phone = user.Phone;
            existingUser.Address = user.Address;

            // Lưu thay đổi
            await _service.UpdateUserAsync(existingUser);

            return Json(new { success = true });
        }

    }
}
