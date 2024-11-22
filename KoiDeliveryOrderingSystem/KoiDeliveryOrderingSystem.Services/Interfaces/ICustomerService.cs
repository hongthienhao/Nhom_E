using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface ICustomerService
    {
        Task<IEnumerable<User>> GetAllCustomersAsync();
        Task<User> GetCustomerByIdAsync(int id);
        Task AddCustomerAsync(string username, string email, string password, string confirmPassword, string phone, string address);
        Task UpdateCustomerAsync(User user);
        Task DeleteCustomerAsync(int id);
        Task LockCustomerAccountAsync(int id);
        Task UnlockCustomerAccountAsync(int id);
    }
}
