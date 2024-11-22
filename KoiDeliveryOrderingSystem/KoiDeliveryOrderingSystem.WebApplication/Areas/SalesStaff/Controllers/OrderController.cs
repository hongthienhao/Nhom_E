using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.SalesStaff.Controllers
{
    [Area("SalesStaff")]
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
            // Đảm bảo phương thức trả về danh sách List<Order>
            var orders = await _service.GetAllOrdersAsync();
            if (orders == null || !orders.Any())
            {
                orders = new List<Order>(); // Tránh lỗi nếu danh sách null
            }
            return View(orders);
        }

        // Hiển thị chi tiết đơn hàng
        public async Task<IActionResult> Details(int id)
        {
            var order = await _service.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }

        // Cập nhật trạng thái đơn hàng
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int id, string newStatus)
        {
            await _service.UpdateOrderStatusAsync(id, newStatus);
            return RedirectToAction("Index");
        }

        // Xóa đơn hàng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteOrderAsync(id);
            return RedirectToAction("Index");
        }
    }
}
