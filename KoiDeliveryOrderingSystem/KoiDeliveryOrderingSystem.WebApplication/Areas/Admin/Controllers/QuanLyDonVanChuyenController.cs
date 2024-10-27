using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// Đảm bảo bạn đã thêm namespace đúng
using System.Linq;
using KoiDeliveryOrderingSystem.WebApplication.Data;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyDonVanChuyenController : Controller
    {
        private readonly HtqlkoiContext _context;

        public QuanLyDonVanChuyenController(HtqlkoiContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Lấy danh sách các đơn hàng từ cơ sở dữ liệu
            var orders = _context.Orders.Include(o => o.Customer).ToList();
            return View(orders); // Trả về View hiển thị danh sách đơn hàng
        }
    }
}
