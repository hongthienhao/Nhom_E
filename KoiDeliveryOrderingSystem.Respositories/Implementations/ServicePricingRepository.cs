using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class ServicePricingRepository : IServicePricingRepository
    {
        private readonly HTQLKoiContext _context;

        public ServicePricingRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ServicePricing>> GetAllAsync()
        {
            return await _context.ServicePricings.ToListAsync();
        }

        public async Task<ServicePricing?> GetByIdAsync(int id)
        {
            return await _context.ServicePricings
                                 .Include(sp => sp.Service)
                                 .FirstOrDefaultAsync(sp => sp.PricingId == id);
        }

        public async Task AddAsync(ServicePricing servicePricing)
        {
            await _context.ServicePricings.AddAsync(servicePricing);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ServicePricing servicePricing)
        {
            _context.ServicePricings.Update(servicePricing);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var pricing = await _context.ServicePricings.FindAsync(id);
            if (pricing != null)
            {
                _context.ServicePricings.Remove(pricing);
                await _context.SaveChangesAsync();
            }
        }

        // Triển khai phương thức mới
        public async Task<IEnumerable<ServicePricing>> GetAllPricingWithServicesAsync()
        {
            return await _context.ServicePricings
                                 .Include(sp => sp.Service) // Liên kết với bảng Service
                                 .ToListAsync();
        }
    }
}
