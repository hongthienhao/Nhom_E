using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IFeedbackService
    {
        // Lấy tất cả phản hồi (dành cho admin)
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync();

        // Lấy phản hồi theo ID
        Task<Feedback?> GetFeedbackByIdAsync(int id);

        // Cập nhật phản hồi (dành cho admin)
        Task UpdateFeedbackAsync(Feedback feedback);

        // Xóa phản hồi (dành cho admin)
        Task DeleteFeedbackAsync(int id);

        // Thêm phản hồi mới (dành cho khách hàng)
        Task AddFeedbackAsync(Feedback feedback);

        // Lấy danh sách phản hồi theo trạng thái xử lý (dành cho admin)
        Task<IEnumerable<Feedback>> GetFeedbacksByResolvedStatusAsync(bool isResolved);

        // Lấy danh sách phản hồi theo ID người dùng (dành cho khách hàng)
        Task<IEnumerable<Feedback>> GetFeedbacksByUserIdAsync(int userId);

        // Lấy danh sách phản hồi theo ID dịch vụ (tùy chọn, dành cho admin)
        Task<IEnumerable<Feedback>> GetFeedbacksByServiceIdAsync(int serviceId);
    }
}
