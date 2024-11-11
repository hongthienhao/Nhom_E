using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _repository;

        public OrderService(IOrderRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _repository.GetAllOrdersAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _repository.GetOrderByIdAsync(id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _repository.AddOrderAsync(order);
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _repository.GetOrdersByCustomerIdAsync(customerId);
        }

        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            await _repository.UpdateOrderStatusAsync(id, newStatus);
        }

        public async Task DeleteOrderAsync(int id)
        {
            await _repository.DeleteOrderAsync(id);
        }
    }
}
