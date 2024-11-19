using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IServicePricingRepository
    {
        Task<IEnumerable<ServicePricing>> GetAllAsync(); // Lấy tất cả bảng giá
        Task<ServicePricing?> GetByIdAsync(int id); // Lấy bảng giá theo ID
        Task AddAsync(ServicePricing servicePricing); // Thêm mới bảng giá
        Task UpdateAsync(ServicePricing servicePricing); // Cập nhật bảng giá
        Task DeleteAsync(int id); // Xóa bảng giá theo ID
        Task<IEnumerable<ServicePricing>> GetAllPricingWithServicesAsync(); // Lấy tất cả bảng giá kèm thông tin dịch vụ
    }
}
