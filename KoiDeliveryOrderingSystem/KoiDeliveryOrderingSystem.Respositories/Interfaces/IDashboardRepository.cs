using System.Collections.Generic;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public interface IDashboardRepository
    {
        List<MonthlyReport> GetMonthlyOrderStatistics();
    }
}
