using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync(); // Lấy tất cả dịch vụ
        Task<Service?> GetByIdAsync(int id); // Lấy dịch vụ theo ID
        Task AddAsync(Service service); // Thêm mới dịch vụ
        Task UpdateAsync(Service service); // Cập nhật dịch vụ
        Task DeleteAsync(int id); // Xóa dịch vụ theo ID
    }
}
