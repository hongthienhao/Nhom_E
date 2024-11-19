using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HTQLKoiContext _context;

        public ServiceRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            // Lấy tất cả danh sách dịch vụ
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            // Lấy thông tin dịch vụ theo ID
            return await _context.Services.FindAsync(id);
        }

        public async Task AddAsync(Service service)
        {
            // Thêm mới dịch vụ
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Service service)
        {
            // Cập nhật dịch vụ
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            // Xóa dịch vụ theo ID
            var service = await GetByIdAsync(id);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
