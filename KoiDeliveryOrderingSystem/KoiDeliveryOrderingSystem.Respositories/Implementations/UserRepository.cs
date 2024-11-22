using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly HTQLKoiContext _context;

        public UserRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmailAndPasswordAsync(string email, string password)
        {
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.Password == password &&
                (u.IsActive.HasValue && u.IsActive.Value)); // Kiểm tra tài khoản còn hoạt động
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == userId);
        }

        // Cập nhật thông tin người dùng
        public async Task UpdateAsync(User user)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserId == user.UserId);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Phone = user.Phone;
                existingUser.Address = user.Address;
                existingUser.IsActive = user.IsActive;

                _context.Users.Update(existingUser);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new Exception("Người dùng không tồn tại.");
            }
        }
    }
}
