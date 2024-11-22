using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Services.Implementations;
using KoiDeliveryOrderingSystem.ViewModels;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")] // Xác định Area là Admin
    public class CustomerController : Controller
    {
        private readonly ICustomerService _customerService;
        private readonly IOrderService _orderService;
        public CustomerController(ICustomerService customerService, IOrderService orderService)
        {
            _customerService = customerService;
            _orderService = orderService;
        }

        // Hiển thị danh sách khách hàng
        public async Task<IActionResult> Index()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            return View(customers); // Trả về danh sách khách hàng trong View Index.cshtml
        }
        // Hiển thị thông tin chi tiết khách hàng và lịch sử đơn hàng
        public async Task<IActionResult> Details(int id)
        {
            // Lấy thông tin khách hàng
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            // Lấy danh sách đơn hàng của khách hàng
            var orders = await _orderService.GetOrdersByCustomerIdAsync(id);

            // Tạo ViewModel để truyền vào View
            var viewModel = new CustomerDetailsViewModel
            {
                Customer = customer,
                Orders = orders
            };

            return View(viewModel); // Truyền ViewModel vào View
        }



        // Hiển thị form thêm khách hàng
        [HttpGet]
        public IActionResult Create()
        {
            return View(); // Trả về View Create.cshtml
        }

        // Xử lý thêm khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string username, string email, string password, string confirmPassword, string phone, string address)
        {
            try
            {
                // Gọi service để xử lý thêm khách hàng
                await _customerService.AddCustomerAsync(username, email, password, confirmPassword, phone, address);

                // Sau khi thêm khách hàng thành công, điều hướng đến trang danh sách khách hàng
                return RedirectToAction("Index", "Customer");
            }
            catch (Exception ex)
            {
                // Gửi thông báo lỗi nếu thêm khách hàng thất bại
                TempData["Error"] = ex.Message;
                return View(); // Trả về trang Create để người dùng sửa lỗi
            }
        }

        // Hiển thị form chỉnh sửa thông tin khách hàng
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            return View(customer);
        }

        // Xử lý chỉnh sửa thông tin khách hàng
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(User customer)
        {
            try
            {
                // Kiểm tra nếu mật khẩu rỗng, sẽ không cập nhật mật khẩu
                if (string.IsNullOrWhiteSpace(customer.Password))
                {
                    var existingCustomer = await _customerService.GetCustomerByIdAsync(customer.UserId);
                    customer.Password = existingCustomer.Password; // Giữ mật khẩu cũ nếu không có mật khẩu mới
                }

                await _customerService.UpdateCustomerAsync(customer);
                TempData["Success"] = "Thông tin khách hàng đã được cập nhật.";
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                TempData["Error"] = $"Lỗi khi cập nhật khách hàng: {ex.Message}";
                return View(customer);
            }
        }

        // Hiển thị form xác nhận xóa khách hàng
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            return View(customer); // Trả về View Delete.cshtml
        }

        // Xử lý xóa khách hàng
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _customerService.DeleteCustomerAsync(id); // Gọi Service để xóa khách hàng
            TempData["Success"] = "Khách hàng đã được xóa.";
            return RedirectToAction("Index"); // Chuyển hướng về danh sách khách hàng
        }

        // Khóa tài khoản khách hàng
        public async Task<IActionResult> Lock(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            await _customerService.LockCustomerAccountAsync(id); // Gọi Service để khóa tài khoản khách hàng
            TempData["Success"] = "Tài khoản khách hàng đã bị khóa.";
            return RedirectToAction("Index");
        }

        // Mở khóa tài khoản khách hàng
        public async Task<IActionResult> Unlock(int id)
        {
            var customer = await _customerService.GetCustomerByIdAsync(id);
            if (customer == null)
            {
                TempData["Error"] = "Không tìm thấy khách hàng.";
                return RedirectToAction("Index");
            }

            await _customerService.UnlockCustomerAccountAsync(id); // Gọi Service để mở khóa tài khoản khách hàng
            TempData["Success"] = "Tài khoản khách hàng đã được mở khóa.";
            return RedirectToAction("Index");
        }
    }
}
