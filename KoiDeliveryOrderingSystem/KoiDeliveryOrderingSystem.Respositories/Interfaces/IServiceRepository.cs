using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        // Lấy tất cả dịch vụ
        Task<IEnumerable<Service>> GetAllAsync();

        // Lấy dịch vụ theo ID
        Task<Service> GetByIdAsync(int serviceId);

        // Thêm một dịch vụ mới
        Task AddAsync(Service service);

        // Cập nhật thông tin dịch vụ
        Task UpdateAsync(Service service);

        // Xóa dịch vụ theo ID
        Task DeleteAsync(int serviceId);
    }
}
