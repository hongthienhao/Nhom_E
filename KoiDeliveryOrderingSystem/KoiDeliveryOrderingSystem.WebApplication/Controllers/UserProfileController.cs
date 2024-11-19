using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Để sử dụng Session
using System.Linq; // Để sử dụng Select
using KoiDeliveryOrderingSystem.Repositories; // Đảm bảo namespace cho ViewModel

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;

        public UserProfileController(IUserService userService, IOrderService orderService)
        {
            _userService = userService;
            _orderService = orderService;
        }

        // GET: UserProfile
        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            // Lấy thông tin người dùng
            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            // Lấy các đơn hàng của người dùng
            var orders = await _orderService.GetOrdersByCustomerIdAsync(userId);

            var userProfileViewModel = new UserProfileViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address,
                IsActive = user.IsActive ?? false,
                Orders = orders.Select(o => new OrderViewModel
                {
                    OrderId = o.OrderId,
                    PickupLocation = o.PickupLocation,
                    DeliveryLocation = o.DeliveryLocation,
                    ShippingMethod = o.ShippingMethod,
                    Status = o.Status,
                    TotalPrice = o.TotalPrice ?? 0
                }).ToList()
            };

            return View(userProfileViewModel);
        }

        // GET: UserProfile/Edit
        public async Task<IActionResult> Edit()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var user = await _userService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return NotFound();
            }

            var editUserViewModel = new EditUserViewModel
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email,
                Phone = user.Phone,
                Address = user.Address
            };

            return View(editUserViewModel);
        }

        // POST: UserProfile/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditUserViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var user = await _userService.GetUserByIdAsync(model.UserId);
            if (user == null)
            {
                return NotFound();
            }

            user.Name = model.Name;
            user.Email = model.Email;
            user.Phone = model.Phone;
            user.Address = model.Address;

            try
            {
                await _userService.UpdateUserAsync(user);
                return RedirectToAction("Index");
            }
            catch
            {
                ModelState.AddModelError("", "Lỗi khi cập nhật thông tin người dùng.");
                return View(model);
            }
        }
    }
}
