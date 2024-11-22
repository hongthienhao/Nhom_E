using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Services.Interfaces;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.WebApplication.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class FeedbackController : Controller
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        // Hiển thị danh sách phản hồi
        public async Task<IActionResult> Index()
        {
            var feedbacks = await _feedbackService.GetAllFeedbacksAsync();
            if (feedbacks == null || !feedbacks.Any())
            {
                return View(new List<Feedback>()); // Trả về danh sách rỗng nếu không có phản hồi
            }

            return View(feedbacks);
        }

        // Hiển thị chi tiết phản hồi
        public async Task<IActionResult> Details(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound("Không tìm thấy phản hồi.");
            }

            return View(feedback);
        }

        // Cập nhật trạng thái phản hồi thành "Đã xử lý"
        [HttpPost]
        public async Task<IActionResult> UpdateResolvedStatus(int feedbackId)
        {
            try
            {
                var feedback = await _feedbackService.GetFeedbackByIdAsync(feedbackId);
                if (feedback != null)
                {
                    feedback.Resolved = !feedback.Resolved; // Đảo trạng thái giữa đã xử lý và chưa xử lý
                    await _feedbackService.UpdateFeedbackAsync(feedback); // Cập nhật trong cơ sở dữ liệu
                    TempData["Success"] = "Cập nhật trạng thái thành công!";
                }
                else
                {
                    TempData["Error"] = "Phản hồi không tồn tại!";
                }
                return RedirectToAction("Index", "Feedback", new { Area = "Admin" });
            }
            catch (Exception ex)
            {
                TempData["Error"] = "Lỗi khi cập nhật trạng thái phản hồi: " + ex.Message;
                return RedirectToAction("Index", "Feedback", new { Area = "Admin" });
            }
        }


        // Xóa phản hồi
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0)
            {
                return BadRequest("ID không hợp lệ.");
            }

            var feedback = await _feedbackService.GetFeedbackByIdAsync(id);
            if (feedback == null)
            {
                return NotFound("Không tìm thấy phản hồi.");
            }

            await _feedbackService.DeleteFeedbackAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
