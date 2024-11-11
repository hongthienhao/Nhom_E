using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ServicePricingController : Controller
    {
        private readonly IServicePricingRepository _servicePricingRepository;

        public ServicePricingController(IServicePricingRepository servicePricingRepository)
        {
            _servicePricingRepository = servicePricingRepository;
        }

        public async Task<IActionResult> Index()
        {
            var pricings = await _servicePricingRepository.GetAllAsync();
            return View(pricings);
        }

        public async Task<IActionResult> Details(int id)
        {
            var pricing = await _servicePricingRepository.GetByIdAsync(id);
            if (pricing == null) return NotFound();
            return View(pricing);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ServicePricing pricing)
        {
            if (ModelState.IsValid)
            {
                await _servicePricingRepository.AddAsync(pricing);
                return RedirectToAction(nameof(Index));
            }
            return View(pricing);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var pricing = await _servicePricingRepository.GetByIdAsync(id);
            if (pricing == null) return NotFound();
            return View(pricing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ServicePricing pricing)
        {
            if (ModelState.IsValid)
            {
                await _servicePricingRepository.UpdateAsync(pricing);
                return RedirectToAction(nameof(Index));
            }
            return View(pricing);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            await _servicePricingRepository.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
