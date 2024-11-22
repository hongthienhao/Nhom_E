using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using KoiDeliveryOrderingSystem.Services.Interfaces;

namespace KoiDeliveryOrderingSystem.Services.Implementations
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;

        public FeedbackService(IFeedbackRepository feedbackRepository)
        {
            _feedbackRepository = feedbackRepository;
        }

        // Lấy tất cả phản hồi (cho admin)
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _feedbackRepository.GetAllFeedbacksAsync();
        }

        // Lấy phản hồi theo ID
        public async Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            return await _feedbackRepository.GetFeedbackByIdAsync(id);
        }

        // Cập nhật trạng thái phản hồi (cho admin xử lý phản hồi)
        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            // Gọi đến repository để cập nhật thông tin phản hồi
            await _feedbackRepository.UpdateFeedbackAsync(feedback);
        }

        // Xóa phản hồi (dành cho admin)
        public async Task DeleteFeedbackAsync(int id)
        {
            await _feedbackRepository.DeleteFeedbackAsync(id);
        }

        // Thêm phản hồi mới (cho khách hàng)
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            feedback.Resolved = false; // Phản hồi mặc định là chưa xử lý
            await _feedbackRepository.AddFeedbackAsync(feedback);
        }

        // Lấy danh sách phản hồi theo trạng thái xử lý (cho admin)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByResolvedStatusAsync(bool isResolved)
        {
            return await _feedbackRepository.GetFeedbacksByResolvedStatusAsync(isResolved);
        }

        // Lấy danh sách phản hồi theo ID người dùng (cho khách hàng)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserIdAsync(int userId)
        {
            return await _feedbackRepository.GetFeedbacksByUserIdAsync(userId);
        }

        // Lấy danh sách phản hồi theo dịch vụ (nếu cần lọc theo dịch vụ)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByServiceIdAsync(int serviceId)
        {
            return await _feedbackRepository.GetFeedbacksByServiceIdAsync(serviceId);
        }
    }
}
