using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using KoiDeliveryOrderingSystem.Repositories; // Sử dụng đúng namespace chứa HTQLKoiContext và Order

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuanLyDonVanChuyenController : Controller
    {
        private readonly HTQLKoiContext _context;

        public QuanLyDonVanChuyenController(HTQLKoiContext context)
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
