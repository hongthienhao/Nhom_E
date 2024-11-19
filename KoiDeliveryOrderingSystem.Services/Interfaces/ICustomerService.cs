using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories; // Đảm bảo bạn có đúng namespace cho các entity

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User> GetUserByIdAsync(int id);
        Task<IEnumerable<Order>> GetUserOrdersAsync(int userId); // Thêm phương thức này
        Task UpdateUserAsync(User user);
        Task LockUserAccountAsync(int userId);
        Task UnlockUserAccountAsync(int userId);
    }
}
