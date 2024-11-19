using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface ICustomerRepository
    {
        Task<IEnumerable<User>> GetAllUsersAsync(); // Lấy danh sách tất cả người dùng
        Task<User> GetUserByIdAsync(int id); // Lấy chi tiết người dùng theo ID
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId); // Lấy danh sách đơn hàng của người dùng
        Task UpdateUserAsync(User user); // Cập nhật thông tin người dùng
        Task LockUserAccountAsync(int userId); // Khóa tài khoản người dùng
        Task UnlockUserAccountAsync(int userId); // Mở khóa tài khoản người dùng
    }
}
