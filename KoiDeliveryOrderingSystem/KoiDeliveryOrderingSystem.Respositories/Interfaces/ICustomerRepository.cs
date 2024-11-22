using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        // Lấy danh sách tất cả khách hàng
        Task<IEnumerable<User>> GetAllCustomersAsync();

        // Lấy thông tin khách hàng theo ID
        Task<User> GetCustomerByIdAsync(int id);

        // Thêm một khách hàng mới
        Task AddCustomerAsync(User customer);

        // Cập nhật thông tin khách hàng
        Task UpdateCustomerAsync(User customer);

        // Xóa khách hàng
        Task DeleteCustomerAsync(int id);

        // Khóa tài khoản khách hàng
        Task LockCustomerAccountAsync(int userId);

        // Mở khóa tài khoản khách hàng
        Task UnlockCustomerAccountAsync(int userId);
        Task<bool> IsEmailExistAsync(string email);
    }
}
