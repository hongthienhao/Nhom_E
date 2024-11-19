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
            // Kiểm tra email và mật khẩu, chỉ trả về user nếu tài khoản đang hoạt động
            return await _context.Users.FirstOrDefaultAsync(u =>
                u.Email == email &&
                u.Password == password &&
                (u.IsActive.HasValue && u.IsActive.Value)); // Chuyển nullable bool thành bool
        }

        public async Task<bool> IsEmailExistAsync(string email)
        {
            // Kiểm tra email đã tồn tại chưa
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        public async Task AddUserAsync(User user)
        {
            // Thêm người dùng mới vào cơ sở dữ liệu
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
