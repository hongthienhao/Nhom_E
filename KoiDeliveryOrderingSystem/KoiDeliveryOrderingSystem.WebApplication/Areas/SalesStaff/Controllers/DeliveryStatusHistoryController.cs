using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.SalesStaff.Controllers
{
    [Area("SalesStaff")]
    public class DeliveryStatusHistoryController : Controller
    {
        private readonly IDeliveryStatusHistoryService _statusHistoryService;

        public DeliveryStatusHistoryController(IDeliveryStatusHistoryService statusHistoryService)
        {
            _statusHistoryService = statusHistoryService;
        }

        // Hiển thị danh sách lịch sử trạng thái giao hàng
        public async Task<IActionResult> Index()
        {
            var statusHistories = await _statusHistoryService.GetAllStatusHistoriesAsync();
            return View(statusHistories);
        }

        // Hiển thị chi tiết lịch sử trạng thái giao hàng
        public async Task<IActionResult> Details(int id)
        {
            var statusHistory = await _statusHistoryService.GetStatusHistoryByIdAsync(id);
            if (statusHistory == null)
            {
                return NotFound();
            }
            return View(statusHistory);
        }

        // Thêm lịch sử trạng thái giao hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(DeliveryStatusHistory deliveryStatusHistory)
        {
            if (ModelState.IsValid)
            {
                await _statusHistoryService.AddStatusHistoryAsync(deliveryStatusHistory);
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryStatusHistory);
        }

        // Cập nhật lịch sử trạng thái giao hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DeliveryStatusHistory deliveryStatusHistory)
        {
            if (id != deliveryStatusHistory.StatusId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _statusHistoryService.UpdateStatusHistoryAsync(deliveryStatusHistory);
                return RedirectToAction(nameof(Index));
            }
            return View(deliveryStatusHistory);
        }

        // Xóa lịch sử trạng thái giao hàng
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _statusHistoryService.DeleteStatusHistoryAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
