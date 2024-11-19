using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IServicePricingService
    {
        Task<IEnumerable<ServicePricing>> GetAllServicePricingAsync();  // Lấy tất cả bảng giá dịch vụ
        Task<ServicePricing> GetServicePricingByIdAsync(int pricingId);  // Lấy giá trị theo ID
        Task AddServicePricingAsync(ServicePricing servicePricing);  // Thêm dịch vụ giá mới
        Task UpdateServicePricingAsync(ServicePricing servicePricing);  // Cập nhật giá dịch vụ
        Task DeleteServicePricingAsync(int pricingId);  // Xóa bảng giá dịch vụ
    }
}
