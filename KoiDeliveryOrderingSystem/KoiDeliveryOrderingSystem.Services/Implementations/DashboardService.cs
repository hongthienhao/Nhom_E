using System.Collections.Generic;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories
{
    public class DashboardService : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardService(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }

        public List<MonthlyReport> GetMonthlyOrderStatistics()
        {
            return _dashboardRepository.GetMonthlyOrderStatistics();
        }
    }
}
