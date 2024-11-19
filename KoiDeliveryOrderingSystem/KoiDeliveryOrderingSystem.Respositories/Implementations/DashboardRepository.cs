using System.Linq;
using System.Collections.Generic;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly HTQLKoiContext _context;

        public DashboardRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        public List<MonthlyReport> GetMonthlyOrderStatistics()
        {
            return _context.Orders
                .Where(o => o.OrderDate.HasValue) // Lọc các đơn hàng có ngày đặt
                .GroupBy(o => new { o.OrderDate.Value.Year, o.OrderDate.Value.Month })
                .Select(g => new MonthlyReport
                {
                    Year = g.Key.Year,
                    Month = g.Key.Month,
                    Revenue = g.Sum(o => o.TotalPrice ?? 0),
                    TotalOrders = g.Count(),
                    DeliveredOrders = g.Count(o => o.Status == "Delivered"),
                    PendingOrders = g.Count(o => o.Status == "Pending")
                })
                .ToList();
        }
    }
}
