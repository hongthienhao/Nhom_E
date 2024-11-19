using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class DeliveryStatusHistoryService : IDeliveryStatusHistoryService
    {
        private readonly IDeliveryStatusHistoryRepository _statusHistoryRepository;

        public DeliveryStatusHistoryService(IDeliveryStatusHistoryRepository statusHistoryRepository)
        {
            _statusHistoryRepository = statusHistoryRepository;
        }

        public async Task<List<DeliveryStatusHistory>> GetAllStatusHistoriesAsync()
        {
            return await _statusHistoryRepository.GetAllStatusHistoriesAsync();
        }

        public async Task<DeliveryStatusHistory> GetStatusHistoryByIdAsync(int id)
        {
            return await _statusHistoryRepository.GetStatusHistoryByIdAsync(id);
        }

        public async Task AddStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory)
        {
            await _statusHistoryRepository.AddStatusHistoryAsync(deliveryStatusHistory);
        }

        public async Task UpdateStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory)
        {
            await _statusHistoryRepository.UpdateStatusHistoryAsync(deliveryStatusHistory);
        }

        public async Task DeleteStatusHistoryAsync(int id)
        {
            await _statusHistoryRepository.DeleteStatusHistoryAsync(id);
        }
    }
}
