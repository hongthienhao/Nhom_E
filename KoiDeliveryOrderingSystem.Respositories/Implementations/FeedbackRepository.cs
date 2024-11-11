using System.Collections.Generic;
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

        // Lấy tất cả phản hồi
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

        // Cập nhật phản hồi
        public async Task UpdateFeedbackAsync(Feedback feedback)
        {
            _context.Feedbacks.Update(feedback);
            await _context.SaveChangesAsync();
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
    }
}
