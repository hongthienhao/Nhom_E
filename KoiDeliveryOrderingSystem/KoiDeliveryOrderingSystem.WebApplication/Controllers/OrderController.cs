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

        // GET: Tạo đơn hàng
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tạo đơn hàng
        [HttpPost]
        public async Task<IActionResult> Create(Order order)
        {
            int userId = HttpContext.Session.GetInt32("UserId") ?? 0; // Lấy customer_id từ session

            if (userId == 0)
            {
                return RedirectToAction("Login", "Account");
            }
            // Kiểm tra giá trị shipping_method trước khi lưu
            if (order.ShippingMethod != "Hàng không" && order.ShippingMethod != "Đường biển" && order.ShippingMethod != "Đường bộ")
            {
                ModelState.AddModelError("ShippingMethod", "Phương thức vận chuyển không hợp lệ.");
                return View(order); // Trả lại view với lỗi nếu phương thức không hợp lệ
            }
            order.CustomerId = userId;  // Gán customer_id từ session
            order.OrderDate = DateTime.Now;
            order.Status = "Pending"; // Trạng thái ban đầu của đơn hàng
            order.TotalPrice = CalculateTotalPrice(order); // Tính tổng giá trị đơn hàng

            // Thêm dịch vụ bổ sung vào order (nếu có)
            order.AdditionalServices = string.Join(", ", order.AdditionalServices?.Split(',') ?? new string[0]);

            await _orderService.AddOrderAsync(order);

            return RedirectToAction("Index");
        }

        // Hàm tính tổng giá trị đơn hàng (bạn có thể tùy chỉnh thêm logic tính giá)
        private decimal CalculateTotalPrice(Order order)
        {
            decimal basePrice = 1000000; // Giá trị cơ bản
            decimal additionalServicesPrice = 0;

            // Tính thêm giá trị cho các dịch vụ bổ sung
            if (!string.IsNullOrEmpty(order.AdditionalServices))
            {
                if (order.AdditionalServices.Contains("Bảo hiểm hàng hóa"))
                {
                    additionalServicesPrice += (order.TotalPrice ?? 0) * 0.02m;
                }
                if (order.AdditionalServices.Contains("Theo dõi và kiểm tra thường xuyên"))
                {
                    additionalServicesPrice += 1000000; // Dịch vụ theo dõi
                }
                if (order.AdditionalServices.Contains("Dịch vụ xử lý thủ tục hải quan"))
                {
                    additionalServicesPrice += 5000000; // Dịch vụ thủ tục hải quan
                }
            }

            return basePrice + additionalServicesPrice; // Tổng giá trị đơn hàng
        }

        // GET: Danh sách đơn hàng
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

        // GET: Chi tiết đơn hàng
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
