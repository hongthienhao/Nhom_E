using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KoiDeliveryOrderingSystem.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace KoiDeliveryOrderingSystem.Repositories.Implementations
{
    public class FeedbackRepository : IFeedbackRepository
    {
        private readonly HTQLKoiContext _context;

        public FeedbackRepository(HTQLKoiContext context)
        {
            _context = context;
        }

        // Lấy tất cả phản hồi (cho admin)
        public async Task<IEnumerable<Feedback>> GetAllFeedbacksAsync()
        {
            return await _context.Feedbacks
                .Include(f => f.User) // Bao gồm thông tin User
                .Include(f => f.Service) // Bao gồm thông tin Service
                .ToListAsync();
        }

        // Lấy phản hồi theo ID
        public async Task<Feedback?> GetFeedbackByIdAsync(int id)
        {
            return await _context.Feedbacks
                .Include(f => f.User)
                .Include(f => f.Service)
                .FirstOrDefaultAsync(f => f.FeedbackId == id);
        }

        // Cập nhật phản hồi (thường dùng để admin cập nhật trạng thái)
        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            if (feedback == null)
                throw new ArgumentNullException(nameof(feedback));

            try
            {
                // Lấy phản hồi cũ từ cơ sở dữ liệu
                var existingFeedback = await _context.Feedbacks.FindAsync(feedback.FeedbackId);
                if (existingFeedback != null)
                {
                    // Cập nhật các giá trị mới từ đối tượng feedback
                    existingFeedback.Resolved = feedback.Resolved;
                    _context.Entry(existingFeedback).State = EntityState.Modified;
                    await _context.SaveChangesAsync();
                }
                else
                {
                    throw new Exception("Phản hồi không tồn tại.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi cập nhật phản hồi.", ex);
            }
        }

        // Xóa phản hồi
        public async Task DeleteFeedbackAsync(int id)
        {
            var feedback = await _context.Feedbacks.FindAsync(id);
            if (feedback != null)
            {
                _context.Feedbacks.Remove(feedback);
                await _context.SaveChangesAsync();
            }
        }

        // Thêm phản hồi mới (cho khách hàng)
        public async Task AddFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Add(feedback);
            await _context.SaveChangesAsync();
        }

        // Lấy phản hồi theo trạng thái xử lý (cho admin)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByResolvedStatusAsync(bool isResolved)
        {
            return await _context.Feedbacks
                .Where(f => f.Resolved == isResolved)
                .Include(f => f.User)
                .Include(f => f.Service)
                .ToListAsync();
        }

        // Lấy danh sách phản hồi theo người dùng (cho khách hàng)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByUserIdAsync(int userId)
        {
            return await _context.Feedbacks
                .Where(f => f.UserId == userId)
                .Include(f => f.Service)
                .ToListAsync();
        }

        // Lấy phản hồi theo dịch vụ (nếu cần lọc theo dịch vụ mà phản hồi liên quan)
        public async Task<IEnumerable<Feedback>> GetFeedbacksByServiceIdAsync(int serviceId)
        {
            return await _context.Feedbacks
                .Where(f => f.ServiceId == serviceId)
                .Include(f => f.User)
                .ToListAsync();
        }
    }
}
