using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository _repository;

        public CustomerService(ICustomerRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _repository.GetAllUsersAsync();
        }

        public async Task<User> GetUserByIdAsync(int id)
        {
            return await _repository.GetUserByIdAsync(id);
        }

        public async Task<IEnumerable<Order>> GetUserOrdersAsync(int userId)
        {
            return await _repository.GetUserOrdersAsync(userId);
        }

        public async Task UpdateUserAsync(User user)
        {
            await _repository.UpdateUserAsync(user);
        }

        public async Task LockUserAccountAsync(int userId)
        {
            await _repository.LockUserAccountAsync(userId);
        }

        public async Task UnlockUserAccountAsync(int userId)
        {
            await _repository.UnlockUserAccountAsync(userId);
        }
    }
}
