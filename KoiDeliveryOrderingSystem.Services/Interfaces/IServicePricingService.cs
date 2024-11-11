using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IServicePricingService
    {
        Task<IEnumerable<ServicePricing>> GetAllPricingAsync(); // Lấy tất cả bảng giá
        Task<ServicePricing?> GetPricingByIdAsync(int id); // Lấy bảng giá theo ID
        Task AddPricingAsync(ServicePricing servicePricing); // Thêm mới bảng giá
        Task UpdatePricingAsync(ServicePricing servicePricing); // Cập nhật bảng giá
        Task DeletePricingAsync(int id); // Xóa bảng giá
    }
}
