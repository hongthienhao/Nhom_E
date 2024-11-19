using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicePricingController : Controller
    {
        private readonly IServicePricingService _servicePricingService;
        private readonly IServiceService _serviceService;
        private readonly ILogger<ServicePricingController> _logger;

        public ServicePricingController(
            IServicePricingService servicePricingService,
            IServiceService serviceService,
            ILogger<ServicePricingController> logger)
        {
            _servicePricingService = servicePricingService;
            _serviceService = serviceService;
            _logger = logger;
        }

        // Hiển thị danh sách bảng giá
        public async Task<IActionResult> Index()
        {
            try
            {
                var pricings = await _servicePricingService.GetAllPricingWithServicesAsync();
                return View(pricings);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi tải danh sách bảng giá: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể tải danh sách bảng giá.";
                return RedirectToAction("Error", "Home");
            }
        }

        // Hiển thị chi tiết bảng giá
        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var pricing = await _servicePricingService.GetPricingByIdAsync(id);
                if (pricing == null)
                {
                    TempData["ErrorMessage"] = "Bảng giá không tồn tại!";
                    return RedirectToAction(nameof(Index));
                }
                return View(pricing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi hiển thị chi tiết bảng giá {id}: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể hiển thị chi tiết bảng giá.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Hiển thị form tạo mới
        public async Task<IActionResult> Create()
        {
            try
            {
                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name");
                return View();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi tải danh sách dịch vụ: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể hiển thị form tạo mới.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Xử lý tạo mới
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicePricing pricing)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name");
                return View(pricing);
            }

            try
            {
                await _servicePricingService.AddPricingAsync(pricing);
                TempData["SuccessMessage"] = "Thêm bảng giá thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi thêm bảng giá: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể thêm bảng giá.";
                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name");
                return View(pricing);
            }
        }

        // Hiển thị form chỉnh sửa
        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var pricing = await _servicePricingService.GetPricingByIdAsync(id);
                if (pricing == null)
                {
                    TempData["ErrorMessage"] = "Bảng giá không tồn tại!";
                    return RedirectToAction(nameof(Index));
                }

                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", pricing.ServiceId);
                return View(pricing);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi tải form chỉnh sửa bảng giá {id}: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể hiển thị form chỉnh sửa.";
                return RedirectToAction(nameof(Index));
            }
        }

        // Xử lý chỉnh sửa
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServicePricing pricing)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", pricing.ServiceId);
                return View(pricing);
            }

            try
            {
                await _servicePricingService.UpdatePricingAsync(pricing);
                TempData["SuccessMessage"] = "Cập nhật bảng giá thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi cập nhật bảng giá {pricing.PricingId}: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể cập nhật bảng giá.";
                ViewBag.Services = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", pricing.ServiceId);
                return View(pricing);
            }
        }

        // Xử lý xóa bảng giá
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _servicePricingService.DeletePricingAsync(id);
                TempData["SuccessMessage"] = "Xóa bảng giá thành công!";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError($"Lỗi khi xóa bảng giá {id}: {ex.Message}");
                TempData["ErrorMessage"] = "Không thể xóa bảng giá.";
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
