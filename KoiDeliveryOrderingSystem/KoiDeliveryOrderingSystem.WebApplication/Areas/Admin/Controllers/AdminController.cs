using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
        private readonly IDashboardService _dashboardService;

        public AdminController(IDashboardService dashboardService)
        {
            _dashboardService = dashboardService;
        }

        [HttpGet]
        public IActionResult Dashboard()
        {
            var monthlyReports = _dashboardService.GetMonthlyOrderStatistics();

            ViewBag.MonthlyReports = monthlyReports;

            return View();
        }
    }
}
