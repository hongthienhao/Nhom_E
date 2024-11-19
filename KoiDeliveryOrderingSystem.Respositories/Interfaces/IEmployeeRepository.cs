using System.Collections.Generic;
using System.Threading.Tasks;

namespace KoiDeliveryOrderingSystem.Repositories.Interfaces
{
    public interface IEmployeeRepository
    {
        // Lấy danh sách nhân viên với role_id = 2 (Sales Staff) hoặc role_id = 3 (Delivery Staff)
        Task<IEnumerable<User>> GetEmployeesAsync();

        // Lấy thông tin chi tiết nhân viên theo ID
        Task<User> GetEmployeeByIdAsync(int id);

        // Thêm nhân viên mới
        Task AddEmployeeAsync(User user);

        // Cập nhật thông tin nhân viên
        Task UpdateEmployeeAsync(User user);

        // Xóa nhân viên theo ID
        Task DeleteEmployeeAsync(int id);

        // Khóa tài khoản nhân viên (đặt IsActive = false)
        Task LockEmployeeAccountAsync(int id);

        // Mở khóa tài khoản nhân viên (đặt IsActive = true)
        Task UnlockEmployeeAccountAsync(int id);
    }
}
