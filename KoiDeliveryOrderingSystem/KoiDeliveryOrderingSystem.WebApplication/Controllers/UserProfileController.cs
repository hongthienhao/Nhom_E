using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using System;
using System.Collections.Generic;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class UserProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IOrderService _orderService;
        private readonly IFeedbackService _feedbackService;
        private readonly IServiceRepository _serviceRepository; // Thêm _serviceRepository

        public UserProfileController(
            IUserService userService,
            IOrderService orderService,
            IFeedbackService feedbackService,
            IServiceRepository serviceRepository)
        {
            _userService = userService;
            _orderService = orderService;
            _feedbackService = feedbackService;
            _serviceRepository = serviceRepository;
        }

        // GET: UserProfile/Index
        public async Task<IActionResult> Index()
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
                    TotalPrice = o.TotalPrice ?? 0,
                    Feedback = o.Status == "Delivered" ? new FeedbackViewModel
                    {
                        OrderId = o.OrderId,
                        UserId = user.UserId,
                        Rating = 0,
                        Comment = ""
                    } : null
                }).ToList()
            };

            ViewBag.SuccessMessage = TempData["SuccessMessage"];
            ViewBag.ErrorMessage = TempData["ErrorMessage"];

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
                TempData["SuccessMessage"] = "Thông tin cá nhân đã được cập nhật!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", $"Lỗi khi cập nhật thông tin: {ex.Message}");
                return View(model);
            }
        }

        // GET: UserProfile/Feedback
        public async Task<IActionResult> Feedback(int orderId)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var order = await _orderService.GetOrderByIdAsync(orderId);
            if (order == null || order.CustomerId != userId)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền phản hồi cho đơn hàng này.";
                return RedirectToAction("Index");
            }

            if (order.Status != "Delivered")
            {
                TempData["ErrorMessage"] = "Chỉ có thể phản hồi cho đơn hàng đã giao.";
                return RedirectToAction("Index");
            }

            var services = await _serviceRepository.GetAllAsync();


            var feedbackViewModel = new FeedbackViewModel
            {
                OrderId = orderId,
                UserId = userId,
                Rating = 0,
                Comment = "",
                Services = services.Select(s => new KeyValuePair<int, string>(s.ServiceId, s.Name)).ToList()
            };

            return View(feedbackViewModel);
        }

        // POST: UserProfile/SubmitFeedback
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SubmitFeedback(FeedbackViewModel model)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Thông tin phản hồi không hợp lệ.";
                return View("Feedback", model);
            }

            var order = await _orderService.GetOrderByIdAsync(model.OrderId);
            if (order == null || order.CustomerId != model.UserId)
            {
                TempData["ErrorMessage"] = "Bạn không có quyền phản hồi cho đơn hàng này.";
                return RedirectToAction("Index");
            }

            if (order.Status != "Delivered")
            {
                TempData["ErrorMessage"] = "Chỉ có thể phản hồi cho đơn hàng đã giao.";
                return RedirectToAction("Index");
            }

            var feedback = new Feedback
            {
                UserId = model.UserId,
                OrderId = model.OrderId,
                Rating = model.Rating,
                Comment = model.Comment,
                CreatedAt = DateTime.Now,
                Resolved = false
            };

            try
            {
                await _feedbackService.AddFeedbackAsync(feedback);
                TempData["SuccessMessage"] = "Cảm ơn bạn đã gửi phản hồi!";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Lỗi khi gửi phản hồi: {ex.Message}";
                return View("Feedback", model);
            }
        }
    }
}
