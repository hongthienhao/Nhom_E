using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.DeliveringStaff.Controllers
{
    [Area("DeliveringStaff")]
    public class DeliveryController : Controller
    {
        private readonly IDeliveryService _deliveryService;

        public DeliveryController(IDeliveryService deliveryService)
        {
            _deliveryService = deliveryService;
        }

        // Hiển thị tất cả các giao hàng
        public async Task<IActionResult> Index()
        {
            var deliveries = await _deliveryService.GetAllDeliveriesAsync();
            return View(deliveries);
        }

        // Xem chi tiết giao hàng
        public async Task<IActionResult> Details(int id)
        {
            var delivery = await _deliveryService.GetDeliveryByIdAsync(id);
            if (delivery == null)
            {
                return NotFound();
            }
            return View(delivery);
        }

        // Thêm giao hàng mới
        [HttpPost]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                await _deliveryService.AddDeliveryAsync(delivery);
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // Cập nhật giao hàng
        [HttpPost]
        public async Task<IActionResult> Update(Delivery delivery)
        {
            if (ModelState.IsValid)
            {
                await _deliveryService.UpdateDeliveryAsync(delivery);
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // Xóa giao hàng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _deliveryService.DeleteDeliveryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
