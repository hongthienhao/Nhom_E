using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

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
            return await _context.Orders.ToListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id)
        {
            return await _context.Orders.FirstOrDefaultAsync(o => o.OrderId == id);
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
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }
    }
}
