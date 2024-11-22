using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class DeliveryRepository : IDeliveryRepository
    {
        private readonly HTQLKoiContext _context;

        public DeliveryRepository(HTQLKoiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        // Lấy tất cả giao hàng
        public async Task<List<Delivery>> GetAllDeliveriesAsync()
        {
            return await _context.Deliveries
                .Include(d => d.Order) // Liên kết với bảng Orders
                .Include(d => d.DeliveryStaff) // Liên kết với bảng Users
                .ToListAsync();
        }

        // Lấy thông tin giao hàng theo ID
        public async Task<Delivery> GetDeliveryByIdAsync(int id)
        {
            return await _context.Deliveries
                .Include(d => d.Order) // Liên kết với bảng Orders
                .Include(d => d.DeliveryStaff) // Liên kết với bảng Users
                .FirstOrDefaultAsync(d => d.DeliveryId == id);
        }

        // Thêm giao hàng mới
        public async Task AddDeliveryAsync(Delivery delivery)
        {
            if (delivery == null)
                throw new ArgumentNullException(nameof(delivery));

            await _context.Deliveries.AddAsync(delivery);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin giao hàng
        public async Task UpdateDeliveryAsync(Delivery delivery)
        {
            if (delivery == null)
                throw new ArgumentNullException(nameof(delivery));

            _context.Deliveries.Update(delivery);
            await _context.SaveChangesAsync();
        }

        // Xóa giao hàng
        public async Task DeleteDeliveryAsync(int id)
        {
            var delivery = await GetDeliveryByIdAsync(id);
            if (delivery != null)
            {
                _context.Deliveries.Remove(delivery);
                await _context.SaveChangesAsync();
            }
        }
    }
}
