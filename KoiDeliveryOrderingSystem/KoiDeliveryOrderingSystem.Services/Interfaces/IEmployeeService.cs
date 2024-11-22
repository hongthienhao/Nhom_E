using KoiDeliveryOrderingSystem.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<IEnumerable<User>> GetAllEmployeesAsync();
        Task<User> GetEmployeeByIdAsync(int id);
        Task AddEmployeeAsync(string username, string email, string password, string confirmPassword, string phone, string address, int roleId);
        Task UpdateEmployeeAsync(User user);
        Task DeleteEmployeeAsync(int id);
       
        Task LockEmployeeAccountAsync(int id); // Khóa tài khoản nhân viên
        Task UnlockEmployeeAccountAsync(int id); // Mở khóa tài khoản nhân viên
    }
}
