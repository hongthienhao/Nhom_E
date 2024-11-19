using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly HTQLKoiContext _context;

        public DeliveryRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy tất cả các giao hàng
        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Deliveries
                .Include(d => d.DeliveryStaff)
                .Include(d => d.Order)
                .ToListAsync();
        }

        // Lấy giao hàng theo id
        public async Task<Delivery> GetDeliveryByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.DeliveryStaff)
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.DeliveryId == id);
        }

        // Thêm giao hàng mới
        public async Task AddDeliveryAsync(Delivery delivery)
        {
            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        // Cập nhật giao hàng
        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        // Xóa giao hàng
        public async Task DeleteDeliveryAsync(int id)
        {
            var delivery = await _context.Deliveries.FindAsync(id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
