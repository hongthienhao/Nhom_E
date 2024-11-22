using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Implementations;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")] // Xác định Area là Admin
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        // Hiển thị danh sách nhân viên
        public async Task<IActionResult> Index()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return View(employees); // Trả về danh sách nhân viên trong View Index.cshtml
        }

        // Hiển thị thông tin chi tiết nhân viên
        public async Task<IActionResult> Details(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction("Index");
            }

            return View(employee); // Trả về View Details.cshtml
        }


        // Hiển thị form thêm nhân viên
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Trả về View Create.cshtml
        }

        // Xử lý thêm nhân viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string username, string email, string password, string confirmPassword, string phone, string address, int roleId)
        {
            try
            {
                // Gọi service để xử lý thêm nhân viên
                await _employeeService.AddEmployeeAsync(username, email, password, confirmPassword, phone, address, roleId);

                // Sau khi thêm nhân viên thành công, điều hướng đến trang danh sách nhân viên
                return RedirectToAction("Index", "Employee"); // Điều hướng tới trang danh sách nhân viên
            }
            catch (Exception ex)
            {
                // Gửi thông báo lỗi nếu thêm nhân viên thất bại
                TempData["Error"] = ex.Message;
                return View(); // Trả về trang Create để người dùng sửa lỗi
            }
        }


        // Hiển thị form chỉnh sửa thông tin nhân viên
        public async Task<IActionResult> Edit(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction("Index");
            }

            var model = new EditEmployeeViewModel
            {
                UserId = employee.UserId,
                Name = employee.Name,
                Email = employee.Email,
                Phone = employee.Phone,
                Address = employee.Address,
                RoleId = employee.RoleId ?? 0

            };

            return View(model);
        }



        // Xử lý chỉnh sửa thông tin nhân viên
        [HttpPost]
        public async Task<IActionResult> Edit(User user)
        {
            try
            {
                // Kiểm tra nếu mật khẩu rỗng, sẽ không cập nhật mật khẩu
                if (string.IsNullOrWhiteSpace(user.Password))
                {
                    var existingUser = await _employeeService.GetEmployeeByIdAsync(user.UserId);
                    user.Password = existingUser.Password;  // Giữ mật khẩu cũ nếu không có mật khẩu mới
                }

                await _employeeService.UpdateEmployeeAsync(user);
                TempData["Success"] = "Thông tin nhân viên đã được cập nhật.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi khi cập nhật nhân viên: {ex.Message}";
                return View(user);
            }
        }




        // Hiển thị form xác nhận xóa nhân viên
        public async Task<IActionResult> Delete(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction("Index");
            }

            return View(employee); // Trả về View Delete.cshtml
        }

        // Xử lý xóa nhân viên
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _employeeService.DeleteEmployeeAsync(id); // Gọi Service để xóa nhân viên
            TempData["Success"] = "Nhân viên đã được xóa.";
            return RedirectToAction("Index"); // Chuyển hướng về danh sách nhân viên
        }

        // Khóa tài khoản nhân viên
        public async Task<IActionResult> Lock(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction("Index");
            }

            await _employeeService.LockEmployeeAccountAsync(id); // Gọi Service để khóa nhân viên
            TempData["Success"] = "Tài khoản nhân viên đã bị khóa.";
            return RedirectToAction("Index");
        }

        // Mở khóa tài khoản nhân viên
        public async Task<IActionResult> Unlock(int id)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(id);
            if (employee == null)
            {
                TempData["Error"] = "Không tìm thấy nhân viên.";
                return RedirectToAction("Index");
            }

            await _employeeService.UnlockEmployeeAccountAsync(id); // Gọi Service để mở khóa nhân viên
            TempData["Success"] = "Tài khoản nhân viên đã được mở khóa.";
            return RedirectToAction("Index");
        }
    }
}
