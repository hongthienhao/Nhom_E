using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IServicePricingService
    {
        Task<IEnumerable<ServicePricing>> GetAllPricingWithServicesAsync(); // Lấy tất cả bảng giá kèm dịch vụ
        Task<ServicePricing?> GetPricingByIdAsync(int id); // Lấy bảng giá theo ID
        Task AddPricingAsync(ServicePricing servicePricing); // Thêm mới bảng giá
        Task UpdatePricingAsync(ServicePricing servicePricing); // Cập nhật bảng giá
        Task DeletePricingAsync(int id); // Xóa bảng giá
    }
}
