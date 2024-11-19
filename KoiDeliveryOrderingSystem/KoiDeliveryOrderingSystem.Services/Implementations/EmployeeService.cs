using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        // Lấy danh sách nhân viên
        public async Task<IEnumerable<User>> GetAllEmployeesAsync()
        {
            return await _employeeRepository.GetEmployeesAsync();
        }

        // Lấy thông tin chi tiết nhân viên
        public async Task<User> GetEmployeeByIdAsync(int id)
        {
            return await _employeeRepository.GetEmployeeByIdAsync(id);
        }

        // Thêm nhân viên mới
        public async Task AddEmployeeAsync(User user)
        {
            await _employeeRepository.AddEmployeeAsync(user);
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateEmployeeAsync(User user)
        {
            await _employeeRepository.UpdateEmployeeAsync(user);
        }

        // Xóa nhân viên
        public async Task DeleteEmployeeAsync(int id)
        {
            await _employeeRepository.DeleteEmployeeAsync(id);
        }

        // Khóa tài khoản nhân viên
        public async Task LockEmployeeAccountAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                employee.IsActive = false; // Đặt trạng thái tài khoản là không hoạt động
                await _employeeRepository.UpdateEmployeeAsync(employee); // Lưu thay đổi
            }
        }

        // Mở khóa tài khoản nhân viên
        public async Task UnlockEmployeeAccountAsync(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            if (employee != null)
            {
                employee.IsActive = true; // Đặt trạng thái tài khoản là hoạt động
                await _employeeRepository.UpdateEmployeeAsync(employee); // Lưu thay đổi
            }
        }
    }
}
