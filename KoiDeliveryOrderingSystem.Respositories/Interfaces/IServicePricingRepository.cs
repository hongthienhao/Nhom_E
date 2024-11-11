using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IServicePricingRepository
    {
        Task<IEnumerable<ServicePricing>> GetAllAsync(); // Lấy tất cả bảng giá
        Task<ServicePricing?> GetByIdAsync(int id);      // Lấy bảng giá theo ID
        Task AddAsync(ServicePricing servicePricing);   // Thêm bảng giá mới
        Task UpdateAsync(ServicePricing servicePricing);// Cập nhật bảng giá
        Task DeleteAsync(int id);                       // Xóa bảng giá
    }
}
