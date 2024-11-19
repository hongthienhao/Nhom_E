using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.DeliveringStaff.Controllers
{
    [Area("DeliveringStaff")]
    public class OrderDetailController : Controller
    {
        private readonly IOrderDetailService _orderDetailService;

        // Constructor: Inject IOrderDetailService vào controller
        public OrderDetailController(IOrderDetailService orderDetailService)
        {
            _orderDetailService = orderDetailService;
        }

        // Hiển thị danh sách chi tiết đơn hàng
        public async Task<IActionResult> Index()
        {
            var orderDetails = await _orderDetailService.GetAllOrderDetailsAsync();
            return View(orderDetails);
        }

        // Xử lý thêm chi tiết đơn hàng
        [HttpPost]
        public async Task<IActionResult> Create(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.AddOrderDetailAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // Xử lý cập nhật chi tiết đơn hàng
        [HttpPost]
        public async Task<IActionResult> Update(OrderDetail orderDetail)
        {
            if (ModelState.IsValid)
            {
                await _orderDetailService.UpdateOrderDetailAsync(orderDetail);
                return RedirectToAction(nameof(Index));
            }
            return View(orderDetail);
        }

        // Xử lý xóa chi tiết đơn hàng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderDetailService.DeleteOrderDetailAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
