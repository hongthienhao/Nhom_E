using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

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
        public IActionResult Create()
        {
            return View(); // Trả về View Create.cshtml
        }

        // Xử lý thêm nhân viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Vui lòng kiểm tra lại thông tin nhập.";
                return View(user);
            }

            await _employeeService.AddEmployeeAsync(user); // Gọi Service để thêm nhân viên
            TempData["Success"] = "Nhân viên đã được thêm thành công.";
            return RedirectToAction("Index"); // Chuyển hướng về danh sách nhân viên
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

            return View(employee); // Trả về View Edit.cshtml
        }

        // Xử lý chỉnh sửa thông tin nhân viên
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User user)
        {
            if (!ModelState.IsValid)
            {
                TempData["Error"] = "Vui lòng kiểm tra lại thông tin nhập.";
                return View(user);
            }

            await _employeeService.UpdateEmployeeAsync(user); // Gọi Service để cập nhật nhân viên
            TempData["Success"] = "Thông tin nhân viên đã được cập nhật.";
            return RedirectToAction("Index"); // Chuyển hướng về danh sách nhân viên
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
