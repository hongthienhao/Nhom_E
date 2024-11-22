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
                TempData["ErrorMessage"] = "Không tìm thấy giao hàng.";
                return RedirectToAction(nameof(Index));
            }
            return View(delivery);
        }

        // Thêm giao hàng mới
        [HttpPost]
        public async Task<IActionResult> Create(Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ.";
                return View(delivery);
            }

            await _deliveryService.AddDeliveryAsync(delivery);
            TempData["SuccessMessage"] = "Thêm giao hàng mới thành công.";
            return RedirectToAction(nameof(Index));
        }

        // Cập nhật thông tin giao hàng
        [HttpPost]
        public async Task<IActionResult> Update(Delivery delivery)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Thông tin không hợp lệ.";
                return View(delivery);
            }

            await _deliveryService.UpdateDeliveryAsync(delivery);
            TempData["SuccessMessage"] = "Cập nhật giao hàng thành công.";
            return RedirectToAction(nameof(Index));
        }

        // Cập nhật trạng thái giao hàng
        [HttpPost]
        public async Task<IActionResult> UpdateStatus(int deliveryId, string newStatus)
        {
            try
            {
                await _deliveryService.UpdateDeliveryStatusAsync(deliveryId, newStatus);
                TempData["SuccessMessage"] = "Cập nhật trạng thái giao hàng thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }

        // Xóa giao hàng
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _deliveryService.DeleteDeliveryAsync(id);
                TempData["SuccessMessage"] = "Xóa giao hàng thành công.";
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = $"Có lỗi xảy ra: {ex.Message}";
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
