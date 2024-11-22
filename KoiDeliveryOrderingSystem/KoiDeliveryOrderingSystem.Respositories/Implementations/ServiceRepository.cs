using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System;

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
            try
            {
                return await _context.Services.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách dịch vụ.", ex);
            }
        }

        // Lấy dịch vụ theo ID
        public async Task<Service?> GetByIdAsync(int serviceId)
        {
            try
            {
                return await _context.Services.AsNoTracking()
                                               .FirstOrDefaultAsync(s => s.ServiceId == serviceId);
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi lấy dịch vụ với ID {serviceId}.", ex);
            }
        }

        // Thêm một dịch vụ mới
        public async Task AddAsync(Service service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service), "Dịch vụ không được để trống.");

            try
            {
                await _context.Services.AddAsync(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thêm dịch vụ.", ex);
            }
        }

        // Cập nhật thông tin dịch vụ
        public async Task UpdateAsync(Service service)
        {
            if (service == null)
                throw new ArgumentNullException(nameof(service), "Dịch vụ không được để trống.");

            try
            {
                var existingService = await _context.Services.FindAsync(service.ServiceId);
                if (existingService == null)
                    throw new KeyNotFoundException("Dịch vụ không tồn tại.");

                _context.Entry(existingService).CurrentValues.SetValues(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi cập nhật dịch vụ với ID {service.ServiceId}.", ex);
            }
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteAsync(int serviceId)
        {
            try
            {
                var service = await _context.Services.FindAsync(serviceId);
                if (service == null)
                    throw new KeyNotFoundException("Dịch vụ không tồn tại.");

                _context.Services.Remove(service);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception($"Lỗi khi xóa dịch vụ với ID {serviceId}.", ex);
            }
        }

        // Lấy danh sách dịch vụ theo danh sách ID
        public async Task<List<Service>> GetServicesByIdsAsync(IEnumerable<int> serviceIds)
        {
            if (serviceIds == null || !serviceIds.Any())
                return new List<Service>();

            try
            {
                return await _context.Services
                                     .Where(s => serviceIds.Contains(s.ServiceId))
                                     .AsNoTracking()
                                     .ToListAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy danh sách dịch vụ theo ID.", ex);
            }
        }
    }
}
