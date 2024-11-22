using System;
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
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context không thể null.");
        }

        // Lấy danh sách nhân viên
        public async Task<IEnumerable<User>> GetEmployeesAsync()
        {
            return await _context.Users
                .Where(u => u.RoleId == 2 || u.RoleId == 3) // Chỉ lấy nhân viên có RoleId là 2 hoặc 3
                .ToListAsync();
        }
        // Kiểm tra nếu email đã tồn tại
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Lấy thông tin chi tiết nhân viên
        public async Task<User> GetEmployeeByIdAsync(int id)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.UserId == id);
        }

        // Thêm một nhân viên mới (giống như logic đăng ký)
        public async Task AddEmployeeAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Thông tin nhân viên không được để trống.");

            // Kiểm tra các điều kiện đầu vào (giống như đăng ký người dùng)
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Tên nhân viên không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email nhân viên không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Phone))
                throw new ArgumentException("Số điện thoại không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Address))
                throw new ArgumentException("Địa chỉ không được để trống.");

            // Kiểm tra xem email đã tồn tại chưa
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new ArgumentException("Email này đã được đăng ký.");

            // Kiểm tra vai trò hợp lệ
            if (user.RoleId != 2 && user.RoleId != 3)
                throw new ArgumentException("Vai trò không hợp lệ. Vui lòng chọn Sales Staff hoặc Delivery Staff.");

            // Thêm nhân viên vào cơ sở dữ liệu
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateEmployeeAsync(User user)
        {
            var existingEmployee = await _context.Users.FindAsync(user.UserId);
            if (existingEmployee == null)
            {
                throw new Exception("Không tìm thấy nhân viên để cập nhật.");
            }

            // Nếu mật khẩu không thay đổi, giữ lại mật khẩu cũ
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = existingEmployee.Password;
            }

            // Cập nhật thông tin từ đối tượng user
            existingEmployee.Name = user.Name;
            existingEmployee.Email = user.Email;
            existingEmployee.Phone = user.Phone;
            existingEmployee.Address = user.Address;
            existingEmployee.RoleId = user.RoleId;
            existingEmployee.Password = user.Password;  // Cập nhật mật khẩu (nếu có thay đổi)

            _context.Users.Update(existingEmployee);
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
                user.IsActive = false; // Đặt trạng thái tài khoản là không hoạt động
                await UpdateEmployeeAsync(user); // Cập nhật thông tin
            }
        }

        // Mở khóa tài khoản nhân viên
        public async Task UnlockEmployeeAccountAsync(int id)
        {
            var user = await GetEmployeeByIdAsync(id);
            if (user != null)
            {
                user.IsActive = true; // Đặt trạng thái tài khoản là hoạt động
                await UpdateEmployeeAsync(user); // Cập nhật thông tin
            }
        }
    }
}
