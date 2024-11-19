using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IServiceService
    {
        Task<IEnumerable<Service>> GetAllServicesAsync(); // Lấy tất cả dịch vụ
        Task<Service?> GetServiceByIdAsync(int id); // Lấy dịch vụ theo ID
        Task AddServiceAsync(Service service); // Thêm mới dịch vụ
        Task UpdateServiceAsync(Service service); // Cập nhật dịch vụ
        Task DeleteServiceAsync(int id); // Xóa dịch vụ
    }
}
