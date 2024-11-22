using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Implementations;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly IOrderService _service;

        public OrderController(IOrderService service)
        {
            _service = service;
        }

        // Hiển thị danh sách đơn hàng
        public async Task<IActionResult> Index()
        {
            var orders = await _service.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                TempData["Message"] = "Không có đơn hàng nào để hiển thị.";
                return View(new List<Order>()); // Trả về danh sách rỗng
            }
            return View(orders);
        }

        // Hiển thị chi tiết đơn hàng
        public async Task<IActionResult> Details(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null)
            {
                TempData["Error"] = $"Không tìm thấy đơn hàng với ID: {id}";
                return RedirectToAction("Index"); // Quay về danh sách nếu không tìm thấy
            }
            return View(order);
        }

        // Cập nhật trạng thái đơn hàng
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                TempData["Error"] = "Trạng thái không hợp lệ.";
                return RedirectToAction("Details", new { id });
            }

            try
            {
                await _service.UpdateOrderStatusAsync(id, newStatus);
                TempData["Success"] = "Cập nhật trạng thái thành công.";
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Đã xảy ra lỗi: {ex.Message}";
            }

            return RedirectToAction("Details", new { id });
        }

        // Xóa đơn hàng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _service.DeleteOrderAsync(id);
                TempData["Success"] = "Đơn hàng đã được xóa thành công.";
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Đã xảy ra lỗi khi xóa đơn hàng: {ex.Message}";
            }

            return RedirectToAction("Index");
        }

        // Hiển thị form chỉnh sửa thông tin vận chuyển
        public async Task<IActionResult> EditShipping(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null)
            {
                TempData["Error"] = $"Không tìm thấy đơn hàng với ID: {id}";
                return RedirectToAction("Index");
            }
            return View(order); // Trả về form chỉnh sửa thông tin vận chuyển
        }

        // Cập nhật thông tin vận chuyển
        [HttpPost]
        public async Task<IActionResult> EditShipping(Order order)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Dữ liệu nhập không hợp lệ. Vui lòng kiểm tra lại.";
                return View(order); // Trả lại form nếu dữ liệu không hợp lệ
            }

            try
            {
                await _service.UpdateOrderShippingInfoAsync(order);
                TempData["Success"] = "Thông tin vận chuyển đã được cập nhật thành công.";
            }
            catch (KeyNotFoundException ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Đã xảy ra lỗi khi cập nhật: {ex.Message}";
                return View(order);
            }

            return RedirectToAction("Details", new { id = order.OrderId });
        }

    }
}
