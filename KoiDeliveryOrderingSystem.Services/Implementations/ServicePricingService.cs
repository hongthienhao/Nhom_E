using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class ServicePricingService : IServicePricingService
    {
        private readonly IServicePricingRepository _repository;

        public ServicePricingService(IServicePricingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<ServicePricing>> GetAllPricingAsync()
        {
            return await _repository.GetAllAsync();
        }

        public async Task<ServicePricing?> GetPricingByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddPricingAsync(ServicePricing servicePricing)
        {
            await _repository.AddAsync(servicePricing);
        }

        public async Task UpdatePricingAsync(ServicePricing servicePricing)
        {
            await _repository.UpdateAsync(servicePricing);
        }

        public async Task DeletePricingAsync(int id)
        {
            await _repository.DeleteAsync(id);
        }
    }
}
