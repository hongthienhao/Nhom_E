using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;

        public CustomerService(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        // Lấy danh sách khách hàng
        public async Task<IEnumerable<User>> GetAllCustomersAsync()
        {
            return await _customerRepository.GetAllCustomersAsync();
        }

        // Lấy thông tin chi tiết khách hàng
        public async Task<User> GetCustomerByIdAsync(int id)
        {
            return await _customerRepository.GetCustomerByIdAsync(id);
        }

        // Thêm khách hàng mới
        public async Task AddCustomerAsync(string username, string email, string password, string confirmPassword, string phone, string address)
        {
            // Kiểm tra các điều kiện đầu vào
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Tên khách hàng không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new ArgumentException("Email không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                throw new ArgumentException("Mật khẩu không được để trống.");
            }

            if (password != confirmPassword)
            {
                throw new ArgumentException("Mật khẩu và xác nhận mật khẩu không khớp.");
            }

            if (await _customerRepository.IsEmailExistAsync(email))
            {
                throw new ArgumentException("Email này đã được đăng ký.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new ArgumentException("Số điện thoại không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new ArgumentException("Địa chỉ không được để trống.");
            }

            // Tạo đối tượng khách hàng mới
            var customer = new User
            {
                Name = username,
                Email = email,
                Password = password,  // Mật khẩu không mã hóa
                Phone = phone,
                Address = address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            // Thêm khách hàng vào cơ sở dữ liệu
            await _customerRepository.AddCustomerAsync(customer);
        }

        // Cập nhật thông tin khách hàng
        public async Task UpdateCustomerAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                // Nếu không thay đổi mật khẩu, giữ mật khẩu cũ
                var existingCustomer = await _customerRepository.GetCustomerByIdAsync(user.UserId);
                if (existingCustomer != null)
                {
                    user.Password = existingCustomer.Password;
                }
            }

            await _customerRepository.UpdateCustomerAsync(user);
        }

        // Xóa khách hàng
        public async Task DeleteCustomerAsync(int id)
        {
            await _customerRepository.DeleteCustomerAsync(id);
        }

        // Khóa tài khoản khách hàng
        public async Task LockCustomerAccountAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                customer.IsActive = false; // Đặt trạng thái tài khoản là không hoạt động
                await _customerRepository.UpdateCustomerAsync(customer); // Lưu thay đổi
            }
        }

        // Mở khóa tài khoản khách hàng
        public async Task UnlockCustomerAccountAsync(int id)
        {
            var customer = await _customerRepository.GetCustomerByIdAsync(id);
            if (customer != null)
            {
                customer.IsActive = true; // Đặt trạng thái tài khoản là hoạt động
                await _customerRepository.UpdateCustomerAsync(customer); // Lưu thay đổi
            }
        }
    }
}
