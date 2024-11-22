using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
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

        // Thêm phương thức cập nhật trạng thái giao hàng
        public async Task UpdateDeliveryStatusAsync(int deliveryId, string newStatus)
        {
            // Xác định các trạng thái hợp lệ
            var validStatuses = new[] { "Đang chờ", "Đang giao", "Đã giao" };
            if (!validStatuses.Contains(newStatus))
                throw new ArgumentException("Trạng thái không hợp lệ.", nameof(newStatus));

            // Tìm bản ghi giao hàng cần cập nhật
            var delivery = await _deliveryRepository.GetDeliveryByIdAsync(deliveryId);
            if (delivery == null)
                throw new KeyNotFoundException($"Không tìm thấy giao hàng với ID: {deliveryId}");

            // Cập nhật trạng thái giao hàng
            delivery.DeliveryStatus = newStatus;
            delivery.UpdatedAt = DateTime.Now;

            // Lưu thay đổi
            await _deliveryRepository.UpdateDeliveryAsync(delivery);
        }
    }
}
