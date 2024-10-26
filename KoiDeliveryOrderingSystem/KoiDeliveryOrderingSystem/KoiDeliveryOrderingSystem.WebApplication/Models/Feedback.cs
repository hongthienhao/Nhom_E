namespace KoiDeliveryOrderingSystem.Models
{
    public class Feedback
    {
        public int FeedbackId { get; set; }
        public int UserId { get; set; }
        public int ServiceId { get; set; }
        public int Rating { get; set; }
        public string Comment { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Service Service { get; set; }
    }
}
