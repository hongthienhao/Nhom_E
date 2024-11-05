using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync();
        Task<Order> GetOrderByIdAsync(int id);
        Task AddOrderAsync(Order order);
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId); // Thêm phương thức này
    }
}
