using System;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class AccountService : IAccountService
    {
        private readonly IUserRepository _userRepository;

        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> LoginAsync(string email, string password)
        {
            if (string.IsNullOrWhiteSpace(email) || string.IsNullOrWhiteSpace(password))
            {
                throw new Exception("Email và mật khẩu không được để trống.");
            }

            var user = await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
            if (user == null)
            {
                throw new Exception("Email hoặc mật khẩu không đúng hoặc tài khoản đã bị vô hiệu hóa.");
            }

            return user;
        }

        public async Task RegisterAsync(string username, string email, string password, string confirmPassword, string phone, string address)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new Exception("Tên người dùng không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(email))
            {
                throw new Exception("Email không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(password) || string.IsNullOrWhiteSpace(confirmPassword))
            {
                throw new Exception("Mật khẩu không được để trống.");
            }

            if (password != confirmPassword)
            {
                throw new Exception("Mật khẩu và xác nhận mật khẩu không khớp.");
            }

            if (await _userRepository.IsEmailExistAsync(email))
            {
                throw new Exception("Email này đã được đăng ký.");
            }

            if (string.IsNullOrWhiteSpace(phone))
            {
                throw new Exception("Số điện thoại không được để trống.");
            }

            if (string.IsNullOrWhiteSpace(address))
            {
                throw new Exception("Địa chỉ không được để trống.");
            }

            var user = new User
            {
                Name = username,
                Email = email,
                Password = password, // Mật khẩu chưa mã hóa, chỉ dùng minh họa
                Phone = phone,
                Address = address,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            await _userRepository.AddUserAsync(user);
        }
    }
}
