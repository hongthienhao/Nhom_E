using System.Collections.Generic;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories;

namespace KoiDeliveryOrderingSystem.Services.Interfaces
{
    public interface IFeedbackService
    {
        Task<IEnumerable<Feedback>> GetAllFeedbacksAsync(); // Lấy tất cả phản hồi
        Task<Feedback?> GetFeedbackByIdAsync(int id); // Lấy phản hồi theo ID
        Task UpdateFeedbackAsync(Feedback feedback); // Cập nhật phản hồi (trạng thái)
        Task DeleteFeedbackAsync(int id); // Xóa phản hồi
    }
}
