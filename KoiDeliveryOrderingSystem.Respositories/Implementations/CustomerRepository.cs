using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using KoiDeliveryOrderingSystem.Repositories; // Đảm bảo đúng namespace cho các entity như User và Order
using KoiDeliveryOrderingSystem.Repositories.Interfaces;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HTQLKoiContext _context;

        public CustomerRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy danh sách tất cả người dùng
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Lấy thông tin người dùng theo ID, bao gồm cả đơn hàng của người dùng
        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Orders)
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        // Lấy danh sách đơn hàng của người dùng theo userId
        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _context.Orders
                .Where(o => o.CustomerId == userId)
                .ToListAsync();
        }

        // Cập nhật thông tin người dùng
        public async Task UpdateUserAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Khóa tài khoản người dùng
        public async Task LockUserAccountAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = false;
                await _context.SaveChangesAsync();
            }
        }

        // Mở khóa tài khoản người dùng
        public async Task UnlockUserAccountAsync(int userId)
        {
            var user = await _context.Users.FindAsync(userId);
            if (user != null)
            {
                user.IsActive = true;
                await _context.SaveChangesAsync();
            }
        }
    }
}
