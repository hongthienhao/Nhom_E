using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class DeliveryStatusHistoryRepository : IDeliveryStatusHistoryRepository
    {
        private readonly HTQLKoiContext _context;

        public DeliveryStatusHistoryRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy tất cả lịch sử trạng thái giao hàng
        public async Task<List<DeliveryStatusHistory>> GetAllStatusHistoriesAsync()
        {
            return await _context.DeliveryStatusHistories
                .Include(d => d.Order)
                .ToListAsync();
        }

        // Lấy lịch sử trạng thái giao hàng theo ID
        public async Task<DeliveryStatusHistory> GetStatusHistoryByIdAsync(int id)
        {
            return await _context.DeliveryStatusHistories
                .Include(d => d.Order)
                .FirstOrDefaultAsync(d => d.StatusId == id);
        }

        // Thêm lịch sử trạng thái giao hàng
        public async Task AddStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory)
        {
            await _context.DeliveryStatusHistories.AddAsync(deliveryStatusHistory);
            await _context.SaveChangesAsync();
        }

        // Cập nhật lịch sử trạng thái giao hàng
        public async Task UpdateStatusHistoryAsync(DeliveryStatusHistory deliveryStatusHistory)
        {
            _context.DeliveryStatusHistories.Update(deliveryStatusHistory);
            await _context.SaveChangesAsync();
        }

        // Xóa lịch sử trạng thái giao hàng
        public async Task DeleteStatusHistoryAsync(int id)
        {
            var statusHistory = await _context.DeliveryStatusHistories.FindAsync(id);
            if (statusHistory != null)
            {
                _context.DeliveryStatusHistories.Remove(statusHistory);
                await _context.SaveChangesAsync();
            }
        }
    }
}
