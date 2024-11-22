using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using System;
using System.Threading.Tasks;

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
        public async Task AddEmployeeAsync(string username, string email, string password, string confirmPassword, string phone, string address, int roleId)
        {
            // Kiểm tra các điều kiện đầu vào
            if (string.IsNullOrWhiteSpace(username))
            {
                throw new ArgumentException("Tên nhân viên không được để trống.");
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

            if (await _employeeRepository.IsEmailExistAsync(email))
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

            if (roleId != 2 && roleId != 3)
            {
                throw new ArgumentException("Vai trò không hợp lệ. Vui lòng chọn Sales Staff hoặc Delivery Staff.");
            }

            // Tạo đối tượng nhân viên mới
            var employee = new User
            {
                Name = username,
                Email = email,
                Password = password,  // Mật khẩu không mã hóa
                Phone = phone,
                Address = address,
                RoleId = roleId,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now,
                IsActive = true
            };

            // Thêm nhân viên vào cơ sở dữ liệu
            await _employeeRepository.AddEmployeeAsync(employee);
        }

        // Cập nhật thông tin nhân viên
        public async Task UpdateEmployeeAsync(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Password))
            {
                // Nếu không thay đổi mật khẩu, giữ mật khẩu cũ
                var existingEmployee = await _employeeRepository.GetEmployeeByIdAsync(user.UserId);
                if (existingEmployee != null)
                {
                    user.Password = existingEmployee.Password;
                }
            }

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
