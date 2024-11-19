using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly HTQLKoiContext _context;

        public ServiceRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy tất cả dịch vụ
        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service> GetByIdAsync(int serviceId)
        {
            return await _context.Services
                                 .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
        }

        // Thêm một dịch vụ mới
        public async Task AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin dịch vụ
        public async Task UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            await _context.SaveChangesAsync();
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteAsync(int serviceId)
        {
            var service = await _context.Services.FindAsync(serviceId);
            if (service != null)
            {
                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
        }
    }
}
