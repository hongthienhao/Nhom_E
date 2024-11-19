using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IServiceService
    {
        // Phương thức để lấy tất cả dịch vụ
        Task<IEnumerable<Service>> GetAllServicesAsync();

        // Phương thức để lấy dịch vụ theo ID
        Task<Service> GetServiceByIdAsync(int serviceId);

        // Phương thức để thêm dịch vụ mới
        Task AddServiceAsync(Service service);

        // Phương thức để cập nhật dịch vụ
        Task UpdateServiceAsync(Service service);

        // Phương thức để xóa dịch vụ theo ID
        Task DeleteServiceAsync(int serviceId);
    }
}
