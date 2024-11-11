using System.Collections.Generic;
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

        public async Task<IEnumerable<Order>> GetAllOrdersAsync()
        {
            return await _context.Orders.Include(o => o.Customer).ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.Include(o => o.Customer).FirstOrDefaultAsync(o => o.OrderId == id);
        }

        public async Task AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
        {
            return await _context.Orders
                                 .Where(o => o.CustomerId == customerId)
                                 .ToListAsync();
        }

        public async Task UpdateOrderStatusAsync(int id, string newStatus)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                order.Status = newStatus;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteOrderAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order != null)
            {
                _context.Orders.Remove(order);
                await _context.SaveChangesAsync();
            }
        }
    }
}
