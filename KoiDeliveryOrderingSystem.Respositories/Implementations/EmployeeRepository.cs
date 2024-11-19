using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly HTQLKoiContext _context;

        public EmployeeRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy danh sách nhân viên
        public async Task<IEnumerable<User>> GetEmployeesAsync()
        {
            return await _context.Users
                .Where(u => u.RoleId == 2 || u.RoleId == 3) // Chỉ lấy nhân viên có RoleId là 2 hoặc 3
                .ToListAsync();
        }

        // Lấy thông tin chi tiết nhân viên
        public async Task<User> GetEmployeeByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        // Thêm nhân viên mới
        public async Task AddEmployeeAsync(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateEmployeeAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }

        // Xóa nhân viên
        public async Task DeleteEmployeeAsync(int id)
        {
            var user = await GetEmployeeByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Khóa tài khoản nhân viên
        public async Task LockEmployeeAccountAsync(int id)
        {
            var user = await GetEmployeeByIdAsync(id);
            if (user != null)
            {
                user.IsActive = false; // Đặt trạng thái là không hoạt động
                await UpdateEmployeeAsync(user); // Cập nhật thông tin
            }
        }

        // Mở khóa tài khoản nhân viên
        public async Task UnlockEmployeeAccountAsync(int id)
        {
            var user = await GetEmployeeByIdAsync(id);
            if (user != null)
            {
                user.IsActive = true; // Đặt trạng thái là hoạt động
                await UpdateEmployeeAsync(user); // Cập nhật thông tin
            }
        }
    }
}
