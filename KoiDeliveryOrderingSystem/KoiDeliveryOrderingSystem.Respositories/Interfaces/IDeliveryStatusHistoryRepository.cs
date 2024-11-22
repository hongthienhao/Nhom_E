using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IDeliveryStatusHistoryRepository
    {
        Task<List<DeliveryStatusHistory>> GetAllStatusHistoriesAsync();
        Task<DeliveryStatusHistory> GetStatusHistoryByIdAsync(int id);
        Task AddStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory);
        Task UpdateStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory);
        Task DeleteStatusHistoryAsync(int id);
    }
}
