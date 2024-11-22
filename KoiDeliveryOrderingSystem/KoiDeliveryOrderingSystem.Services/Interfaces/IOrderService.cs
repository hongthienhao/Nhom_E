using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrdersAsync(); // Lấy tất cả đơn hàng (admin)
        Task<Order> GetOrderByIdAsync(int id); // Lấy chi tiết đơn hàng
        Task AddOrderAsync(Order order); // Thêm đơn hàng (khách hàng)
        Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId); // Lấy đơn hàng của khách hàng
        Task UpdateOrderStatusAsync(int id, string newStatus); // Cập nhật trạng thái đơn hàng (admin)
        Task DeleteOrderAsync(int id); // Xóa đơn hàng (admin)
        Task<List<Service>> GetServicesByOrderIdAsync(int orderId);
        Task UpdateOrderShippingInfoAsync(Order order);

    }
}