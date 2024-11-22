using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IServicePricingRepository
    {
        Task<IEnumerable<ServicePricing>> GetAllAsync();  // Lấy tất cả bảng giá dịch vụ
        Task<ServicePricing> GetByIdAsync(int pricingId);  // Lấy bảng giá dịch vụ theo ID
        Task AddAsync(ServicePricing servicePricing);  // Thêm dịch vụ giá mới
        Task UpdateAsync(ServicePricing servicePricing);  // Cập nhật giá dịch vụ
        Task DeleteAsync(int pricingId);  // Xóa bảng giá dịch vụ
    }
}
