using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class ServicePricingService : IServicePricingService
    {
        private readonly IServicePricingRepository _servicePricingRepository;

        public ServicePricingService(IServicePricingRepository servicePricingRepository)
        {
            _servicePricingRepository = servicePricingRepository;
        }

        public async Task<IEnumerable<ServicePricing>> GetAllServicePricingAsync()
        {
            return await _servicePricingRepository.GetAllAsync();
        }

        public async Task<ServicePricing> GetServicePricingByIdAsync(int pricingId)
        {
            return await _servicePricingRepository.GetByIdAsync(pricingId);
        }

        public async Task AddServicePricingAsync(ServicePricing servicePricing)
        {
            await _servicePricingRepository.AddAsync(servicePricing);
        }

        public async Task UpdateServicePricingAsync(ServicePricing servicePricing)
        {
            await _servicePricingRepository.UpdateAsync(servicePricing);
        }

        public async Task DeleteServicePricingAsync(int pricingId)
        {
            await _servicePricingRepository.DeleteAsync(pricingId);
        }
    }
}
