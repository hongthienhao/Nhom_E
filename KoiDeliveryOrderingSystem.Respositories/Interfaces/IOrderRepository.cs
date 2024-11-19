using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(); // Lấy tất cả đơn hàng (dành cho admin)
        Task<Order> GetOrderByIdAsync(int id); // Lấy chi tiết đơn hàng
        Task AddOrderAsync(Order order); // Thêm đơn hàng (dành cho khách hàng)
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId); // Lấy danh sách đơn hàng của một khách hàng
        Task UpdateOrderStatusAsync(int id, string newStatus); // Cập nhật trạng thái đơn hàng (dành cho admin)
        Task DeleteOrderAsync(int id); // Xóa đơn hàng (dành cho admin)
    }
}