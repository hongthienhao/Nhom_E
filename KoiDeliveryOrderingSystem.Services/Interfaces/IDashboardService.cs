using System.Collections.Generic;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public interface IDashboardService
    {
        List<MonthlyReport> GetMonthlyOrderStatistics();
    }
}
