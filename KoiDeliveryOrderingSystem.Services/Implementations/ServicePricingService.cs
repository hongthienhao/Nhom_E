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

        public async Task<IEnumerable<ServicePricing>> GetAllPricingWithServicesAsync()
        {
            // Lấy tất cả bảng giá kèm thông tin dịch vụ
            return await _repository.GetAllPricingWithServicesAsync();
        }

        public async Task<ServicePricing?> GetPricingByIdAsync(int id)
        {
            // Lấy thông tin bảng giá theo ID
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddPricingAsync(ServicePricing servicePricing)
        {
            // Thêm mới bảng giá
            await _repository.AddAsync(servicePricing);
        }

        public async Task UpdatePricingAsync(ServicePricing servicePricing)
        {
            // Cập nhật bảng giá
            await _repository.UpdateAsync(servicePricing);
        }

        public async Task DeletePricingAsync(int id)
        {
            // Xóa bảng giá
            await _repository.DeleteAsync(id);
        }
    }
}
