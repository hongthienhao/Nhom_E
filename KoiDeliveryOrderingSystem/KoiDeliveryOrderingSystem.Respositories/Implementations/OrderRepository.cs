using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HTQLKoiContext _context;

        public OrderRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy tất cả đơn hàng
        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders
                .Include(o => o.Customer) // Bao gồm thông tin khách hàng
                .ToListAsync();
        }

        // Lấy đơn hàng theo ID
        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders
                .Include(o => o.Customer) // Bao gồm thông tin khách hàng
                .FirstOrDefaultAsync(o => o.OrderId == id);
        }

        // Thêm đơn hàng mới
        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        // Lấy danh sách đơn hàng theo ID khách hàng
        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == customerId)
                .Include(o => o.Customer) // Bao gồm thông tin khách hàng
                .ToListAsync();
        }

        // Cập nhật trạng thái đơn hàng
        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }

        // Cập nhật thông tin đơn hàng
        public async Task UpdateOrderShippingInfoAsync(Order order)
        {
            var existingOrder = await _context.Orders.FindAsync(order.OrderId);

            if (existingOrder == null)
            {
                throw new KeyNotFoundException("Không tìm thấy đơn hàng để cập nhật.");
            }

            // Cập nhật các thông tin liên quan đến vận chuyển
            existingOrder.PickupLocation = order.PickupLocation;
            existingOrder.DeliveryLocation = order.DeliveryLocation;
            existingOrder.ShippingMethod = order.ShippingMethod;
            existingOrder.TotalWeight = order.TotalWeight;
            existingOrder.TotalQuantity = order.TotalQuantity;

            // Chỉ cần cập nhật các thông tin trên mà không thay đổi UpdatedAt

            _context.Orders.Update(existingOrder);

            // Lưu các thay đổi vào cơ sở dữ liệu
            await _context.SaveChangesAsync();
        }



        // Xóa đơn hàng
        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }

        // Lấy thông tin chi tiết đơn hàng (OrderDetails) theo OrderId
        public async Task<List<OrderDetail>> GetOrderDetailsByOrderIdAsync(int orderId)
        {
            return await _context.OrderDetails
                .Where(od => od.OrderId == orderId)
                .Include(od => od.Service) // Bao gồm thông tin dịch vụ nếu cần
                .ToListAsync();
        }
    }
}
