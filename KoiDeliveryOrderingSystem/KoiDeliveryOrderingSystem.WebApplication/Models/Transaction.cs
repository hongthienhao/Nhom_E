namespace KoiDeliveryOrderingSystem.Models
{
    public class Transaction
    {
        public int TransactionId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal Amount { get; set; }
        public DateTime CreatedAt { get; set; }

        public User User { get; set; }
        public Order Order { get; set; }
    }
}
