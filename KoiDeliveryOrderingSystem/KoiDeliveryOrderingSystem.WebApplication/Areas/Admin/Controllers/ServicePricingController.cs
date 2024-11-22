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
        private readonly IServiceService _serviceService; // Để lấy danh sách dịch vụ
        private readonly ILogger<ServicePricingController> _logger;

        public ServicePricingController(IServicePricingService servicePricingService, IServiceService serviceService, ILogger<ServicePricingController> logger)
        {
            _servicePricingService = servicePricingService;
            _serviceService = serviceService;
            _logger = logger;
        }

        // GET: Admin/ServicePricing
        public async Task<IActionResult> Index()
        {
            var pricingList = await _servicePricingService.GetAllServicePricingAsync();
            return View(pricingList);
        }

        // GET: Admin/ServicePricing/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var pricing = await _servicePricingService.GetServicePricingByIdAsync(id);
            if (pricing == null)
            {
                _logger.LogError($"ServicePricing with id {id} not found.");
                return NotFound();
            }
            return View(pricing);
        }

        // GET: Admin/ServicePricing/Create
        public async Task<IActionResult> Create()
        {
            // Lấy danh sách dịch vụ và truyền vào ViewData để sử dụng trong dropdown list
            ViewData["Services"] = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name");
            return View();
        }

        // POST: Admin/ServicePricing/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicePricing servicePricing)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _servicePricingService.AddServicePricingAsync(servicePricing);
                    _logger.LogInformation($"ServicePricing with ID {servicePricing.PricingId} created successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error occurred while creating service pricing.");
                    ModelState.AddModelError(string.Empty, "An error occurred while creating the service pricing.");
                }
            }
            // Nếu có lỗi, truyền lại danh sách dịch vụ vào ViewData
            ViewData["Services"] = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", servicePricing.ServiceId);
            return View(servicePricing);
        }

        // GET: Admin/ServicePricing/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var pricing = await _servicePricingService.GetServicePricingByIdAsync(id);
            if (pricing == null)
            {
                _logger.LogError($"ServicePricing with id {id} not found.");
                return NotFound();
            }

            // Lấy danh sách dịch vụ và truyền vào ViewData
            ViewData["Services"] = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", pricing.ServiceId);
            return View(pricing);
        }

        // POST: Admin/ServicePricing/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ServicePricing servicePricing)
        {
            if (id != servicePricing.PricingId)
            {
                _logger.LogError($"Mismatched ServicePricing ID during editing. Expected: {id}, but got: {servicePricing.PricingId}");
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _servicePricingService.UpdateServicePricingAsync(servicePricing);
                    _logger.LogInformation($"ServicePricing with ID {id} updated successfully.");
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, $"Error occurred while updating service pricing with ID {id}.");
                    ModelState.AddModelError(string.Empty, "An error occurred while updating the service pricing.");
                }
            }

            // Nếu có lỗi, truyền lại danh sách dịch vụ vào ViewData
            ViewData["Services"] = new SelectList(await _serviceService.GetAllServicesAsync(), "ServiceId", "Name", servicePricing.ServiceId);
            return View(servicePricing);
        }

        // GET: Admin/ServicePricing/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var pricing = await _servicePricingService.GetServicePricingByIdAsync(id);
            if (pricing == null)
            {
                _logger.LogError($"ServicePricing with id {id} not found.");
                return NotFound();
            }
            return View(pricing);
        }

        // POST: Admin/ServicePricing/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _servicePricingService.DeleteServicePricingAsync(id);
                _logger.LogInformation($"ServicePricing with ID {id} deleted successfully.");
                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error occurred while deleting service pricing with ID {id}.");
                return View();
            }
        }
    }
}
