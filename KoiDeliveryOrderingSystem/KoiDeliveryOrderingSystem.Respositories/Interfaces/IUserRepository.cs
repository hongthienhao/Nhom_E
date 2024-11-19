using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IUserRepository
    {
        Task<User> GetUserByEmailAndPasswordAsync(string email, string password);
        Task<bool> IsEmailExistAsync(string email);
        Task AddUserAsync(User user);
        Task<User> GetUserByIdAsync(int userId);
        Task UpdateAsync(User user); // Thêm phương thức UpdateAsync
    }
}
