using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceService(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        // Lấy tất cả các dịch vụ
        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            return await _serviceRepository.GetAllAsync();
        }

        // Lấy dịch vụ theo ID
        public async Task<Service> GetServiceByIdAsync(int serviceId)
        {
            return await _serviceRepository.GetByIdAsync(serviceId);
        }

        // Thêm dịch vụ mới
        public async Task AddServiceAsync(Service service)
        {
            await _serviceRepository.AddAsync(service);
        }

        // Cập nhật dịch vụ
        public async Task UpdateServiceAsync(Service service)
        {
            await _serviceRepository.UpdateAsync(service);
        }

        // Xóa dịch vụ theo ID
        public async Task DeleteServiceAsync(int serviceId)
        {
            await _serviceRepository.DeleteAsync(serviceId);
        }
    }
}
