using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HTQLKoiContext _context;

        public CustomerRepository(HTQLKoiContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context), "Context không thể null.");
        }

        // Lấy danh sách tất cả khách hàng
        public async Task<IEnumerable<User>> GetAllCustomersAsync()
        {
            return await _context.Users.ToListAsync();
        }

        // Kiểm tra nếu email đã tồn tại
        public async Task<bool> IsEmailExistAsync(string email)
        {
            return await _context.Users.AnyAsync(u => u.Email == email);
        }

        // Lấy thông tin chi tiết khách hàng
        public async Task<User> GetCustomerByIdAsync(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);
        }


        // Thêm một khách hàng mới
        public async Task AddCustomerAsync(User user)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "Thông tin khách hàng không được để trống.");

            // Kiểm tra các điều kiện đầu vào
            if (string.IsNullOrWhiteSpace(user.Name))
                throw new ArgumentException("Tên khách hàng không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email khách hàng không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Phone))
                throw new ArgumentException("Số điện thoại không được để trống.");

            if (string.IsNullOrWhiteSpace(user.Address))
                throw new ArgumentException("Địa chỉ không được để trống.");

            // Kiểm tra xem email đã tồn tại chưa
            if (await _context.Users.AnyAsync(u => u.Email == user.Email))
                throw new ArgumentException("Email này đã được đăng ký.");

            // Thêm khách hàng vào cơ sở dữ liệu
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerAsync(User user)
        {
            var existingCustomer = await _context.Users.FindAsync(user.UserId);
            if (existingCustomer == null)
                throw new Exception("Không tìm thấy khách hàng để cập nhật.");

            // Nếu mật khẩu không thay đổi, giữ lại mật khẩu cũ
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                user.Password = existingCustomer.Password;
            }

            // Cập nhật thông tin từ đối tượng user
            existingCustomer.Name = user.Name;
            existingCustomer.Email = user.Email;
            existingCustomer.Phone = user.Phone;
            existingCustomer.Address = user.Address;
            existingCustomer.Password = user.Password; // Cập nhật mật khẩu (nếu có thay đổi)

            _context.Users.Update(existingCustomer);
            await _context.SaveChangesAsync();
        }

        // Xóa khách hàng
        public async Task DeleteCustomerAsync(int id)
        {
            var user = await GetCustomerByIdAsync(id);
            if (user != null)
            {
                _context.Users.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        // Khóa tài khoản khách hàng
        public async Task LockCustomerAccountAsync(int id)
        {
            var user = await GetCustomerByIdAsync(id);
            if (user != null && (user.IsActive ?? true)) // Kiểm tra nếu tài khoản đang hoạt động
            {
                user.IsActive = false; // Đặt trạng thái tài khoản là không hoạt động
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }

        // Mở khóa tài khoản khách hàng
        public async Task UnlockCustomerAccountAsync(int id)
        {
            var user = await GetCustomerByIdAsync(id);
            if (user != null && !(user.IsActive ?? false)) // Kiểm tra nếu tài khoản đang bị khóa
            {
                user.IsActive = true; // Đặt trạng thái tài khoản là hoạt động
                _context.Users.Update(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
