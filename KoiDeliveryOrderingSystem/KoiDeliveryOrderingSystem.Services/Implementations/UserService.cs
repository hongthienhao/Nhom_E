using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<User> GetUserByIdAsync(int userId)
        {
            return await _userRepository.GetUserByIdAsync(userId);
        }
        public async Task UpdateUserAsync(User user)
        {
            await _userRepository.UpdateAsync(user);  // Cập nhật thông tin người dùng
        }
        public async Task<User> LoginAsync(string email, string password)
        {
            var user = await _userRepository.GetUserByEmailAndPasswordAsync(email, password);
            if (user == null)
            {
                throw new Exception("Invalid credentials");
            }
            return user;
        }

        public async Task RegisterAsync(string username, string email, string password, string confirmPassword)
        {
            if (password != confirmPassword)
            {
                throw new Exception("Passwords do not match");
            }
            var user = new User
            {
                Name = username,
                Email = email,
                Password = password,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _userRepository.AddUserAsync(user);
        }
    }
}
