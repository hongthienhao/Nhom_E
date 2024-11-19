using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class ServiceService : IServiceService
    {
        private readonly IServiceRepository _repository;

        public ServiceService(IServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Service>> GetAllServicesAsync()
        {
            // Lấy tất cả danh sách dịch vụ
            return await _repository.GetAllAsync();
        }

        public async Task<Service?> GetServiceByIdAsync(int id)
        {
            // Lấy thông tin dịch vụ theo ID
            return await _repository.GetByIdAsync(id);
        }

        public async Task AddServiceAsync(Service service)
        {
            // Thêm mới dịch vụ
            await _repository.AddAsync(service);
        }

        public async Task UpdateServiceAsync(Service service)
        {
            // Cập nhật dịch vụ
            await _repository.UpdateAsync(service);
        }

        public async Task DeleteServiceAsync(int id)
        {
            // Xóa dịch vụ
            await _repository.DeleteAsync(id);
        }
    }
}
