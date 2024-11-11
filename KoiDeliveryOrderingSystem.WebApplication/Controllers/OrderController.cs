using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.WebApplication.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Lấy customer_id từ session

            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            order.CustomerId = userId;  // Gán customer_id
            order.OrderDate = DateTime.Now;
            order.Status = "Pending"; // Trạng thái ban đầu của đơn hàng
            order.TotalPrice = CalculateTotalPrice(order); // Hàm tính tổng giá trị đơn hàng

            await _orderService.AddOrderAsync(order);

            return RedirectToAction("Index");
        }

        private decimal CalculateTotalPrice(Order order)
        {
            // Giả sử có logic để tính tổng giá trị đơn hàng dựa trên các chi tiết hoặc dịch vụ
            return 1000000; // Giá trị ví dụ
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0;
            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }

            var orders = await _orderService.GetOrdersByCustomerIdAsync(userId);
            return View(orders);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            return View(order);
        }
    }
}
