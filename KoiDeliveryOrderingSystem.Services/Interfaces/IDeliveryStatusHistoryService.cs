using KoiDeliveryOrderingSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IDeliveryStatusHistoryService
    {
        Task<List<DeliveryStatusHistory>> GetAllStatusHistoriesAsync();
        Task<DeliveryStatusHistory> GetStatusHistoryByIdAsync(int id);
        Task AddStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory);
        Task UpdateStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory);
        Task DeleteStatusHistoryAsync(int id);
    }
}
