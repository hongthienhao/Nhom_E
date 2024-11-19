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
    public class DeliveryService : IDeliveryService
    {
        private readonly IDeliveryRepository _deliveryRepository;

        public DeliveryService(IDeliveryRepository deliveryRepository)
        {
            _deliveryRepository = deliveryRepository;
        }

        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _deliveryRepository.GetAllDeliveriesAsync();
        }

        public async Task<Delivery> GetDeliveryByIdAsync(int id)
        {
            return await _deliveryRepository.GetDeliveryByIdAsync(id);
        }

        public async Task AddDeliveryAsync(Delivery delivery)
        {
            await _deliveryRepository.AddDeliveryAsync(delivery);
        }

        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            await _deliveryRepository.UpdateDeliveryAsync(delivery);
        }

        public async Task DeleteDeliveryAsync(int id)
        {
            await _deliveryRepository.DeleteDeliveryAsync(id);
        }
    }
}
