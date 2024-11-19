using KoiDeliveryOrderingSystem.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> GetUserByIdAsync(int userId);
        Task<User> LoginAsync(string email, string password);
        Task RegisterAsync(string username, string email, string password, string confirmPassword);
        Task UpdateUserAsync(User user);  // Thêm phương thức cập nhật thông tin người dùng
    }
}
