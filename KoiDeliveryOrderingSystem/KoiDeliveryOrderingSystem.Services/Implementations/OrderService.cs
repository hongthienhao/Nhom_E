using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IServiceRepository _serviceRepository;

        public OrderService(IOrderRepository repository, IServiceRepository serviceRepository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _serviceRepository = serviceRepository ?? throw new ArgumentNullException(nameof(serviceRepository));
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            var orders = await _repository.GetAllOrdersAsync();
            return orders ?? Enumerable.Empty<Order>();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID = {id}");
            }
            return order;
        }

        public async Task AddOrderAsync(Order order)
        {
            ValidateOrder(order); // Kiểm tra logic nghiệp vụ
            await _repository.AddOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            var orders = await _repository.GetOrdersByCustomerIdAsync(customerId);
            return orders ?? Enumerable.Empty<Order>();
        }

        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            if (string.IsNullOrWhiteSpace(newStatus))
            {
                throw new ArgumentException("Trạng thái đơn hàng không được để trống.");
            }

            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID = {id}");
            }

            await _repository.UpdateOrderStatusAsync(id, newStatus);
        }

        public async Task UpdateOrderShippingInfoAsync(Order order)
        {
            await _repository.UpdateOrderShippingInfoAsync(order);
        }


        public async Task DeleteOrderAsync(int id)
        {
            var order = await _repository.GetOrderByIdAsync(id);
            if (order == null)
            {
                throw new KeyNotFoundException($"Không tìm thấy đơn hàng với ID = {id}");
            }

            await _repository.DeleteOrderAsync(id);
        }

        public async Task<List<Service>> GetServicesByOrderIdAsync(int orderId)
        {
            var orderDetails = await _repository.GetOrderDetailsByOrderIdAsync(orderId);
            if (orderDetails == null || !orderDetails.Any())
            {
                throw new KeyNotFoundException($"Không tìm thấy dịch vụ nào cho đơn hàng ID = {orderId}");
            }

            var serviceIds = orderDetails.Select(od => od.ServiceId).Distinct();
            return await _serviceRepository.GetServicesByIdsAsync(serviceIds);
        }

        private void ValidateOrder(Order order)
        {
            if (string.IsNullOrWhiteSpace(order.PickupLocation))
            {
                throw new ArgumentException("Địa chỉ lấy hàng không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(order.DeliveryLocation))
            {
                throw new ArgumentException("Địa chỉ giao hàng không được để trống.");
            }

            if (order.TotalWeight <= 0)
            {
                throw new ArgumentException("Khối lượng phải lớn hơn 0.");
            }

            if (order.TotalQuantity <= 0)
            {
                throw new ArgumentException("Số lượng phải lớn hơn 0.");
            }
        }
    }
}
